using Fluxor;
using WebAPP.Models;
using WebAPP.HttpClients;
using static WebAPP.Pages.Catalog.Store.CatalogStore;

namespace WebAPP.Pages.Catalog.Store;

public class CatalogStore {
    public record CatalogState(List<CatalogModel> Catalogs, bool CanvasIsVisible, CatalogModel Catalog);

    public class CatalogFeature : Feature<CatalogState>
    {
        public override string GetName() => "Catalog";

        protected override CatalogState GetInitialState()
            => new CatalogState(new List<CatalogModel>(), false, new());
    }

    /// <summary>
    /// I recommend a naming convention for Actions in the form of FeatureActivityAction where Feature in this case is 
    /// "Counter", Activity is "Increment", and Action distinguishes the class as an Action. 
    /// The reason for this is that eventually you may have a lot of Features, 
    /// and if many of them have an Action class named public class Initialize {} then there is a decent chance of name clashes and confusion later. 
    /// Naming your Actions this way keeps that from happening and makes it easy to keep track of all the pieces of the Store.
    /// </summary>
    /// <param name="catalogs"></param>
    public record CatalogGetPagedAction(int Limit = 8, int Offset = 0);
    public record CatalogSetPagedAction(List<CatalogModel> Catalogs);

    public record CatalogSetCanvasVisibilityAction(bool CanvasVisibility);

    public record CatalogClearCatalogAction();    
    public record CatalogCreateAction();
    public record CatalogAddAction();

    /// <summary>
    /// Note that this is a static class, and the ReducerMethod is a static method. 
    /// This is intentional since a Reducer method should be a pure method with no side effects. 
    /// It takes in the current State (the CounterState state parameter) and returns a new State with new values based on the Action it's handling. 
    /// The Reducers class itself contains no state.
    /// </summary>
    public static class CounterReducers
    {
        [ReducerMethod]
        public static CatalogState OnGetPaged(CatalogState state, CatalogSetPagedAction action)
        {
            return state with
            {
                Catalogs = action.Catalogs
            };
        }

        [ReducerMethod]
        public static CatalogState OnCanvasVisibilityChange(CatalogState state, CatalogSetCanvasVisibilityAction action)
        {
            return state with
            {
                CanvasIsVisible = action.CanvasVisibility
            };
        }

        [ReducerMethod(typeof(CatalogClearCatalogAction))]
        public static CatalogState OnCatalogCleared(CatalogState state)
        {
            return state with
            {
                Catalog = new()
            };
        }

        [ReducerMethod(typeof(CatalogAddAction))]
        public static CatalogState OnCatalogAdded(CatalogState state)
        {
            return state with
            {
                Catalogs = state.Catalogs.Append(state.Catalog).ToList()
            };
        }
    }
}

public class CatalogEffects
{
    private readonly IState<CatalogState> _state;
    private readonly ICatalogHttpClient _httpClient;

    public CatalogEffects(ICatalogHttpClient httpClient, IState<CatalogState> state)
    {
        _httpClient = httpClient;
        _state = state;
    }

    [EffectMethod]
    public async Task GetPagedCatalogs(CatalogGetPagedAction action, IDispatcher dispatcher)
    {
        var response = await _httpClient.GetAsync(action.Limit, action.Offset, CancellationToken.None);
        if (response.Success)
        {
            var catalogs = response.ActionResult.Items.Select(catalog => (CatalogModel) catalog).ToList();
            dispatcher.Dispatch(new CatalogSetPagedAction(catalogs));
        }
    }

    [EffectMethod(typeof(CatalogCreateAction))]
    public async Task CreateCatalog(IDispatcher dispatcher)
    {
        var response = await _httpClient.CreateAsync(_state.Value.Catalog, CancellationToken.None);
        if (response.Success)
        {
            dispatcher.Dispatch(new CatalogAddAction());
            dispatcher.Dispatch(new CatalogSetCanvasVisibilityAction(false));
            dispatcher.Dispatch(new CatalogClearCatalogAction());
        }
    }
}

