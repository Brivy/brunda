using Brunda.External.PartnerApi.Contracts.Models;

namespace Brunda.External.PartnerApi.Tests.Builder.Models;

public class PageModelBuilder<TResult>
{
    private IReadOnlyCollection<TResult> _results = [];
    private int _currentPage = default;
    private int _totalPages = default;

    public PageModelBuilder<TResult> WithResults(IReadOnlyCollection<TResult> results)
    {
        _results = results;
        return this;
    }

    public PageModelBuilder<TResult> WithCurrentPage(int currentPage)
    {
        _currentPage = currentPage;
        return this;
    }

    public PageModelBuilder<TResult> WithTotalPages(int totalPages)
    {
        _totalPages = totalPages;
        return this;
    }

    public PageModel<TResult> Build()
    {
        return new PageModel<TResult>
        {
            Results = _results,
            CurrentPage = _currentPage,
            TotalPages = _totalPages
        };
    }
}
