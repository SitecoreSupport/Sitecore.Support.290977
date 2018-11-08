using Sitecore.ContentSearch;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Sites;

namespace Sitecore.Support.ContentSearch.ComputedFields
{
  public class UrlLink : Sitecore.ContentSearch.ComputedFields.UrlLink
  {
    public override object ComputeFieldValue(IIndexable indexable)
    {
      Item item = indexable as SitecoreIndexableItem;

      if (item == null)
      {
        return null;
      }

      var siteContext = SiteContextFactory.GetSiteContext("website");

      if (item.Paths.IsMediaItem)
      {
        using (new SiteContextSwitcher(siteContext))
        {
          return MediaManager.GetMediaUrl(item);
        }
      }

      UrlOptions urlOptions = LinkManager.GetDefaultUrlOptions();
      urlOptions.Site = siteContext;
      return LinkManager.GetItemUrl(item, urlOptions);
    }
  }
}