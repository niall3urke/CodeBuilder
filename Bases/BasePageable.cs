using CodeBuilder.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CodeBuilder.Components
{
    public abstract class BasePageable<T> : BaseComponent where T : class
    {

        // =============
        // ===== Fields 
        // =============

        protected ICollection<T> _items;

        protected string _searchTerm;

        protected OrderBy _orderBy;

        protected bool _desc = true;

        protected int _take = 10;

        protected int _skip = 0;

        protected int _page = 1;

        protected int _count;

        // ========================
        // ===== Methods: abstract
        // ========================

        protected abstract Task GetItemsAsync();

        // ==============
        // ===== Methods
        // ==============

        protected async Task SearchClicked() => await GetItemsAsync();

        protected async Task ClearSearchClicked()
        {
            _searchTerm = "";
            await GetItemsAsync();
        }

        protected async Task SearchOnKeyUp(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await GetItemsAsync();
            }
        }

        protected async Task OrderChanged(ChangeEventArgs e)
        {
            if (!GetValue(e, out OrderBy value))
                return;

            _orderBy = value;

            await GetItemsAsync();

            StateHasChanged();
        }

        protected async Task ToggleSort()
        {
            _desc = !_desc;

            await GetItemsAsync();
        }

        protected abstract Func<IQueryable<T>, IOrderedQueryable<T>> GetOrder();

        protected async Task CheckPageIsValidAndLoadItems(IEnumerable<T> items)
        {
            // We change the page when, for example, the user deletes
            // the last item on a page, then we try to move backwards

            // This should always be done after setting the ordered 
            // items, as on the first call _items will be null.

            // We need to check the page is greater than one, because
            // if it's the landing page, and we have no items, we'll
            // end up in a continuous loop leading to a stackoverflow
            if (!items.Any() && _page > 1)
            {
                await ChangePage(new PaginationComponent.PageData(_page - 1, _take));
            }
            else
            {
                _items = items.ToList();
            }

            StateHasChanged();
        }

        protected async Task ChangePage(PaginationComponent.PageData data)
        {
            _page = data.Page;

            _take = data.Take;

            _skip = _take * (_page - 1);

            await GetItemsAsync();
        }


    }
}
