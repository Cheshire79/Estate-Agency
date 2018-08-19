using System;
using System.Collections.Generic;
using System.Linq;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;

namespace EstateAgency.BLL.Services.RealeEstateOrdering
{
    class PairedTextMethod<T>
    {
        public PairedTextMethod(string text, Sorting<T> method)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public string Text;
        public Sorting<T> Method;
    }
    public class RealeEstateSort<T> : IRealeEstateSort<T> where T : class, IRealEstateFieldsForSort
    {
        private Dictionary<SortOrder, PairedTextMethod<T>> _textAndFunctions = new Dictionary<SortOrder, PairedTextMethod<T>>()
        {
            {SortOrder.ByDateNewOld, new PairedTextMethod<T>("By date listed (new – old)", l => l.OrderByDescending(x => x.CreationDate))},
            {SortOrder.ByDateOldNew, new PairedTextMethod<T>("By date listed (old – new)", l => l.OrderBy(x => x.CreationDate))},
            {SortOrder.ByPriceMinMax, new PairedTextMethod<T>("By price (min – max)", l => l.OrderBy(x => x.Price))},
            {SortOrder.ByPriceMaxMin, new PairedTextMethod<T>("By price (max – min)", l => l.OrderByDescending(x => x.Price))},
            {SortOrder.ByTotalAreaMinMax, new PairedTextMethod<T>("Total area (min – max)", l => l.OrderBy(x => x.Area))},
            {SortOrder.ByTotalAreaMaxMin, new PairedTextMethod<T>("Total area (max – min)", l => l.OrderByDescending(x => x.Area))}
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

        public Sorting<T> Sort(SortOrder sortOrder)
        {
            return _textAndFunctions[sortOrder].Method;
        }
    }
}
