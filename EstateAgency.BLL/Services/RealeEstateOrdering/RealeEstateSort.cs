using System;
using System.Collections.Generic;
using System.Linq;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;

namespace EstateAgency.BLL.Services.RealeEstateOrdering
{
    class PairedTextMethod
    {
        public PairedTextMethod(string text, Sorting method)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public string Text;
        public Sorting Method;
    }
    public class RealeEstateSort : IRealeEstateSort
    {
        private Dictionary<SortOrder, PairedTextMethod> _textAndFunctions = new Dictionary<SortOrder, PairedTextMethod>()
        {
            {SortOrder.ByDateNewOld, new PairedTextMethod("By date listed (new – old)", l => l.OrderByDescending(x => x.CreationDate))},
            {SortOrder.ByDateOldNew, new PairedTextMethod("By date listed (old – new)", l => l.OrderBy(x => x.CreationDate))},
            {SortOrder.ByPriceMinMax, new PairedTextMethod("By price (min – max)", l => l.OrderBy(x => x.Price))},
            {SortOrder.ByPriceMaxMin, new PairedTextMethod("By price (max – min)", l => l.OrderByDescending(x => x.Price))},
            {SortOrder.ByTotalAreaMinMax, new PairedTextMethod("Total area (min – max)", l => l.OrderBy(x => x.Area))},
            {SortOrder.ByTotalAreaMaxMin, new PairedTextMethod("Total area (max – min)", l => l.OrderByDescending(x => x.Area))}
        };

        public List<SortOrderDropDownDTO> GetSortingOptionsName()
        {
            List<SortOrderDropDownDTO> result = new List<SortOrderDropDownDTO>();
            foreach (var item in _textAndFunctions)
            {
                result.Add(new SortOrderDropDownDTO() { Id = item.Key, Name = item.Value.Text });
            }
            return result;
        }

        public Sorting Sort(SortOrder sortOrder)
        {
            return _textAndFunctions[sortOrder].Method;
        }
    }
}
