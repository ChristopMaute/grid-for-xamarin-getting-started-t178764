using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DevExpress.Mobile.DataGrid;

namespace HelloGrid
{	
	public partial class MainPage : ContentPage
	{	
		int count;
		public MainPage ()
		{
			InitializeComponent ();

			TestOrdersRepository model = new TestOrdersRepository ();
			BindingContext = model;
		}

        #region #CustomSummary
        void OnCalculateCustomSummary(object sender, CustomSummaryEventArgs e){
			if (e.FieldName.ToString () == "Shipped")
			if (e.IsTotalSummary){
				if (e.SummaryProcess == CustomSummaryProcess.Start) {
					count = 0;
				}
				if (e.SummaryProcess == CustomSummaryProcess.Calculate) {
					if (!(bool)e.FieldValue)
						count++;
					e.TotalValue = count;
				}
			}
		}
        #endregion #CustomSummary

        #region #CustomCell
        void OnCustomizeCell(CustomizeCellEventArgs e)
        {
            if (e.FieldName == "Total" && !e.IsSelected)
            {
                int total = Convert.ToInt32(e.Value);
                if (total < 50)
                    e.ForeColor = Color.Red;
                else if (total > 2000)
                    e.ForeColor = Color.Green;
                e.Handled = true;
            }
        }
        #endregion #CustomCell

        #region #SwipeButtons
        void OnSwipeButtonShowing(object sender, SwipeButtonShowingEventArgs e)
        {
            if ((!(Boolean)grid.GetCellValue(e.RowHandle, "Shipped"))
                && (e.ButtonInfo.ButtonName == "RightButton"))
            {
                e.IsVisible = false;
            }
        }

        void OnSwipeButtonClick(object sender, SwipeButtonEventArgs e)
        {
            if (e.ButtonInfo.ButtonName == "LeftButton")
            {
                DateTime orderDate = (DateTime)grid.GetCellValue(e.RowHandle, "Date");
                string orderDateDay = orderDate.ToString("dddd");
                DisplayAlert("Alert from " + e.ButtonInfo.ButtonName, "Day: " + orderDateDay, "OK");
            }
            if (e.ButtonInfo.ButtonName == "RightButton")
            {
                grid.DeleteRow(e.RowHandle);
            }
        }
        #endregion #SwipeButtons
    }
}

