using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket_mvp.Models;
using Supermarket_mvp.Views;

namespace Supermarket_mvp.Presenters
{
    internal class PayModePresenter
    {
        private IPayModeView view;
        private IPayModeRepository repository;
        private BindingSource payModeBindingSource;
        private IEnumerable<PayModeModel> payModeList;

        public PayModePresenter(IPayModeView view, IPayModeRepository repository)
        {
            this.payModeBindingSource= new BindingSource();

            this.view = view;
            this.repository = repository;

            this.view.SearchEvent += SearchPayMode;
            this.view.AddNewEvent += AddNewPayMode;

            this.view.EditEvent += LoadSelectPayModeToEdit;
            this.view.DeleteEvent += DeleteSelectedPayMode;
            this.view.SaveEvent += SavePayMode;
            this.view.CancelEvent += CancelAction;

            this.view.SetPayModeListBildingSoursce(payModeBindingSource);
           
            
           LoadAllPayModeList();
            this.view.Show();
        }

        

        private void LoadAllPayModeList()
        {
            payModeList = repository.GetAll();
            payModeBindingSource.DataSource = payModeList;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SavePayMode(object? sender, EventArgs e)
        {
            var payMode= new PayModeModel();
            payMode.Id = Convert.ToInt32(view.PayModeId);
            payMode.Name = view.PayModeName;
            payMode.Observation = view.PayModeObservation;

            try
            {
                new Common.ModelDataValidation().Validate(payMode);
                if (view.IsEdit)
                {
                    repository.Edit(payMode);
                    view.Message = "PayMode Edited Succesfuly";
                }
                else
                {
                    repository.add(payMode);
                    view.Message = "PayMode adedd Succesfuly";
             
                }
                view.IsSuccessful = true;
                LoadAllPayModeList();
                CleanViewFields();
            }
            catch(Exception ex) 
            { 
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }   
            
        }
        private void CleanViewFields()
        {
            view.PayModeId = "0";
            view.PayModeName ="";
            view.PayModeObservation = "";
        }

        private void DeleteSelectedPayMode(object? sender, EventArgs e)
        {
            try
            {
                var payMode = (PayModeModel) payModeBindingSource.Current;

                repository.Delete(payMode.Id);
                view.IsSuccessful = true;
                view.Message = "Pay Mode deleted successfully";
                LoadAllPayModeList();
            }
            catch(Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete pay mode";
            }
        }

        private void LoadSelectPayModeToEdit(object? sender, EventArgs e)
        {
            var payMode = (PayModeModel)payModeBindingSource.Current;

            view.PayModeId= payMode.Id.ToString();
            view.PayModeName= payMode.Name;
            view.PayModeObservation= payMode.Observation;

            view.IsEdit = true;
        }

        private void AddNewPayMode(object? sender, EventArgs e)
        {
            MessageBox.Show("Hizo clic en el boton nuevo");
            view.IsEdit = false;
        }

        private void SearchPayMode(object? sender, EventArgs e)
        {
            bool emtyValues = string.IsNullOrWhiteSpace(this.view.SerchValues);
            if (emtyValues==false) 
            {
                payModeList = repository.GetByValues(this.view.SerchValues);

            }
            else 
            {
                payModeList = repository.GetAll();

            }
            payModeBindingSource.DataSource=payModeList;
        }
        
    }
}
