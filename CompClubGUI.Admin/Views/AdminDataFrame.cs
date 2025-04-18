using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using CompClubGUI.Admin.API.Models;
using CompClubGUI.Admin.Views.Controls;
using CompClubGUICore;

namespace CompClubGUI.Admin.Views
{
    public class AdminDataFrame : UserControl
    {
        public virtual bool HasUnsavedChanged() => false;
        public virtual async void UpdateData() { }
    }

    public class AdminDataFrame<T, T2> : AdminDataFrame where T : BaseAdminModel where T2 : new()
    {
        protected List<T> data = [];
        protected List<T> filteredData;

        protected DataFrameDataList dataList;
        protected Grid dataListGrid;

        public AdminDataFrame()
        {
            Loaded += AdminDataFrame_Loaded;
        }

        private void AdminDataFrame_Loaded(object? sender, RoutedEventArgs e)
        {
            dataList = new DataFrameDataList<T, T2>();
            dataListGrid.Children.Add(dataList);
            UpdateData();
        }

        public void UpdateFilters()
        {
            filteredData = [.. data];
            FilterOutData();
            dataList.SetData(filteredData.Cast<object>().ToList());
        }

        public void AddNewElement()
        {
            data.Add(CreateNewElement());

            UpdateFilters();
        }

        private async Task<List<T>> GetData()
        {
            if (AppInfo.SessionToken == null)
                return GenerateDebugData();

            return await GetDataFromServer();
        }

        protected async virtual Task<List<T>> GetDataFromServer()
        {
            throw new NotImplementedException();
        }

        public override async void UpdateData()
        {
            List<T> list = await GetData();
            if (list == null) return;

            // preserve is expanded state
            var expandedElementsIds = data.Where(o => o.IsExpanded).Select(o => o.Id);
            foreach (var elem in list.Where(o => expandedElementsIds.Contains(o.Id)))
                elem.IsExpanded = true;

            // leave only edited (dirty) items
            data.RemoveAll(o => !o.IsDirty);

            // remove all removed items from db
            data.RemoveAll(o => !list.Any(oo => oo.Id == o.Id) && !o.IsNewlyCreated);

            // add items which are not edited
            data.AddRange(list.Where(o => !data.Any(oo => o.Id == oo.Id)));

            // order by id
            data = [.. data.OrderBy(o => o.Id)];
            
            UpdateFilters();
        }

        protected virtual void FilterOutData() { }

        protected virtual T CreateNewElement() => default;

        protected virtual List<T> GenerateDebugData() => [];

        public override bool HasUnsavedChanged() => data.Any(o => o is BaseAdminModel m && m.IsDirty);

    }
}
