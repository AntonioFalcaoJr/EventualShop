$USER = "AntonioFalcaoJr"
$REPO = "EventualShop"

Write-Output "Getting workflows for $USER/$REPO"

$url = "/repos/$USER/$REPO/actions/workflows"
$json = gh api $url | ConvertFrom-Json

Write-Output "Found $($json.total_count) workflows"

$json.workflows | ForEach-Object -Parallel {
    $USER = $using:USER
    $REPO = $using:REPO
    $workflowId = $_.id
    $workflowName = $_.name

    Write-Output "Getting runs for workflow $workflowName, id $workflowId"

    $url = "/repos/$USER/$REPO/actions/workflows/$workflowId/runs"
    $json = gh api -X GET $url
    $runs = $json | ConvertFrom-Json

    Write-Output "Found $($runs.total_count) runs"

    $runs.workflow_runs | ForEach-Object -Parallel {
        $USER = $using:USER
        $REPO = $using:REPO
        $runId = $_.id

        Write-Output "Deleting run $runId"

        $url = "/repos/$USER/$REPO/actions/runs/$runId"
        gh api -X DELETE $url
    }
}