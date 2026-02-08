#!/bin/bash
set -e

cd /c/Projects/EventualShop

# Function to generate commit message based on file path and status
generate_message() {
    local file="$1"
    local status="$2"

    # Extract meaningful parts from the path
    local filename=$(basename "$file" | sed 's/\.[^.]*$//')
    local dir=$(dirname "$file")

    # Determine the service context
    local service=""
    if [[ "$file" == src/Services/* ]]; then
        service=$(echo "$file" | cut -d'/' -f3)
    fi

    # Determine layer (Command/Query)
    local layer=""
    if [[ "$file" == *"/Command/"* ]]; then
        layer="Command"
    elif [[ "$file" == *"/Query/"* ]]; then
        layer="Query"
    fi

    # Determine sublayer
    local sublayer=""
    if [[ "$file" == *"/Domain/"* ]]; then
        sublayer="Domain"
    elif [[ "$file" == *"/Application/"* ]]; then
        sublayer="Application"
    elif [[ "$file" == *"/Infrastructure.EventStore/"* ]]; then
        sublayer="EventStore"
    elif [[ "$file" == *"/Infrastructure.EventBus/"* ]]; then
        sublayer="EventBus"
    elif [[ "$file" == *"/Infrastructure.Projections/"* ]]; then
        sublayer="Projections"
    elif [[ "$file" == *"/Infrastructure.SMTP/"* ]]; then
        sublayer="SMTP"
    elif [[ "$file" == *"/GrpcService/"* ]]; then
        sublayer="GrpcService"
    elif [[ "$file" == *"/WorkerService/"* ]]; then
        sublayer="WorkerService"
    fi

    # Handle specific known files
    case "$file" in
        "Directory.Packages.props")
            echo "Upgrade package versions"
            return ;;
        "global.json")
            echo "Upgrade SDK to .NET 10.0.0"
            return ;;
        "README.md")
            echo "Update README"
            return ;;
        "docker-compose.Development.yaml")
            echo "Add Docker Compose profiles and Elasticsearch service for Development"
            return ;;
        "docker-compose.Staging.yaml")
            echo "Add Docker Compose profiles and Elasticsearch service for Staging"
            return ;;
        "Writerside/"*)
            echo "Add Writerside documentation configuration"
            return ;;
        "Writerside.iml")
            echo "Add Writerside module file"
            return ;;
    esac

    # Handle appsettings files
    if [[ "$file" == *"/appsettings."*".json" ]]; then
        local env=$(echo "$filename" | sed 's/appsettings\.//')
        if [[ -n "$service" ]]; then
            echo "Update ${service} ${layer} ${sublayer} appsettings for ${env}"
        else
            echo "Update appsettings for ${env}"
        fi
        return
    fi

    # Handle deleted files
    if [[ "$status" == "D" ]]; then
        if [[ -n "$service" ]]; then
            echo "Remove ${filename} from ${service} ${layer} ${sublayer}"
        else
            echo "Remove ${filename}"
        fi
        return
    fi

    # Handle new/untracked files
    if [[ "$status" == "?" || "$status" == "A" ]]; then
        if [[ -n "$service" ]]; then
            echo "Add ${filename} to ${service} ${layer} ${sublayer}"
        else
            echo "Add ${filename}"
        fi
        return
    fi

    # Handle modified files with service context
    if [[ -n "$service" ]]; then
        # Specific patterns
        case "$file" in
            *"/ValueObjects/"*)
                echo "Improve ${filename} value object in ${service} ${layer}"
                return ;;
            *"/Aggregates/"*)
                echo "Improve ${filename} aggregate in ${service} ${layer}"
                return ;;
            *"/Entities/"*)
                echo "Improve ${filename} entity in ${service} ${layer}"
                return ;;
            *"/Enumerations/"*)
                echo "Improve ${filename} enumeration in ${service} ${layer}"
                return ;;
            *"/Abstractions/"*)
                echo "Improve ${filename} abstraction in ${service} ${layer}"
                return ;;
            *"/UseCases/"*)
                echo "Improve ${filename} use case in ${service} ${layer}"
                return ;;
            *"/Services/"*"Service"*)
                echo "Improve ${filename} in ${service} ${layer}"
                return ;;
            *"/DependencyInjection/"*)
                echo "Update ${filename} DI configuration in ${service} ${layer} ${sublayer}"
                return ;;
            *"/Consumers/"*)
                echo "Update ${filename} consumer in ${service} ${layer}"
                return ;;
            *"/Configurations/"*)
                echo "Update ${filename} configuration in ${service} ${layer} ${sublayer}"
                return ;;
            *"/Migrations/"*)
                echo "Update ${filename} migration in ${service} ${layer}"
                return ;;
            *"/Contexts/"*)
                echo "Update ${filename} context in ${service} ${layer}"
                return ;;
            *"/Pagination/"*)
                echo "Update ${filename} in ${service} ${layer} ${sublayer}"
                return ;;
            *".csproj")
                echo "Update ${service} ${layer} ${sublayer} project file"
                return ;;
            *"Program.cs")
                echo "Update ${service} ${layer} ${sublayer} program entry point"
                return ;;
            *"Dockerfile")
                echo "Update ${service} ${layer} ${sublayer} Dockerfile"
                return ;;
            *"Exceptions.cs")
                echo "Improve domain exceptions in ${service} ${layer}"
                return ;;
            *"Extensions/"*)
                echo "Improve ${filename} extensions in ${service} ${layer}"
                return ;;
            *)
                echo "Update ${filename} in ${service} ${layer} ${sublayer}"
                return ;;
        esac
    fi

    # Handle Contracts
    if [[ "$file" == src/Contracts/* ]]; then
        local boundary=$(echo "$file" | cut -d'/' -f4)
        echo "Update ${filename} contract in ${boundary}"
        return
    fi

    # Handle Web
    if [[ "$file" == src/Web/* ]]; then
        echo "Update ${filename} in WebAPP"
        return
    fi

    # Handle test files
    if [[ "$file" == test/* ]]; then
        echo "Update ${filename} tests"
        return
    fi

    # Fallback
    echo "Update ${filename}"
}

# Counter
count=0
total=$(git status --porcelain | wc -l)

echo "Processing $total files..."

# Process all files from git status
while IFS= read -r line; do
    # Parse status and filename from porcelain output
    status="${line:0:1}"
    status2="${line:1:1}"
    file="${line:3}"

    # Handle renamed files (shown as "old -> new" in some formats)
    # After reset, renames show as separate D and ?? entries

    # Remove quotes if present
    file=$(echo "$file" | sed 's/^"//;s/"$//')

    # Skip empty lines
    [[ -z "$file" ]] && continue

    # Use the secondary status if primary is space
    if [[ "$status" == " " ]]; then
        status="$status2"
    fi

    # For untracked files, status is ?
    if [[ "$status" == "?" ]]; then
        status="?"
    fi

    count=$((count + 1))

    msg=$(generate_message "$file" "$status")

    echo "[$count/$total] $status $file -> $msg"

    git add -- "$file"
    git commit -m "$msg" --quiet

done < <(git status --porcelain)

echo ""
echo "Done! Committed $count files individually."
