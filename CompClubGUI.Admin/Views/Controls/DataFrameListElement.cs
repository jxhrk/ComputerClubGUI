using Avalonia.Controls;
using CompClubGUI.Admin.API.Models;
using CompClubGUICore;
using System;
using System.Threading.Tasks;

namespace CompClubGUI.Admin.Views.Controls
{
    public class DataFrameListElement<T> : UserControl
        where T : BaseAdminModel
    {
        private AdminDataFrame? parentFrame;
        protected T? model;

        public DataFrameListElement()
        {
            SetParentFrame();

            DataContextChanged += DataFrameListElement_DataContextChanged;
        }

        private void DataFrameListElement_DataContextChanged(object? sender, EventArgs e)
        {
            if (DataContext is T model) this.model = model;
        }

        protected virtual async Task<int> CallCreate(T model) => -1;
        protected virtual async Task<int> CallUpdate(T model) => -1;
        protected virtual async Task<int> CallDelete(T model) => -1;

        public async void Save()
        {
            if (model == null) return;

            int status = 200;

            if (model.IsNewlyCreated)
                status = await CallCreate(model);
            else if (model.IsDirty)
                status = await CallUpdate(model);

            if (status == 200 || status == 201 || AppInfo.SessionToken == null)
            {
                model.IsDirty = false;
                if (model.IsNewlyCreated)
                {
                    model.IsNewlyCreated = false;
                    RemoveElementAndUpdate(model);
                }
            }
        }
        public async void Delete()
        {
            if (model == null) return;

            int status = await CallDelete(model);

            if (status == 200 || status == 204 || AppInfo.SessionToken == null)
                RemoveElementAndUpdate(model);
        }

        private void RemoveElementAndUpdate(T place)
        {
            if (parentFrame == null) return;

            parentFrame.UpdateData();
        }

        private void SetParentFrame()
        {
            if (MainWindow.Instance == null) return;
            if (MainWindow.Instance.FrameContent is not AdminView admin) return;
            if (admin.DataFrame is not AdminDataFrame frame) return;
            parentFrame = frame;
        }
    }
}
