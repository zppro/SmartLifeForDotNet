using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartLife.Client.PensionAgency.SelfService
{
    public partial class ucOrderInfo : UserControl
    {
        public ucOrderInfo()
        {
            InitializeComponent();
        }

        public void LoadOrderInfo(string oldManId)
        {
            lblMT00001SetName.Text = lblMT00002SetName.Text = lblMT00003SetName.Text = "-- -- --";
            lblMT00001SetContent.Text = lblMT00002SetContent.Text = lblMT00003SetContent.Text = "-- -- -- -- -- -- -- -- --";
            
            var meals = Data.BookMeals.Where(item => item.OldManId == oldManId);
            foreach (var meal in meals)
            {
                if (meal.MealType == "00001")
                {
                    lblMT00001SetName.Text = meal.SetMealName;
                    lblMT00001SetContent.Text = meal.SetMealContent;
                }
                else if (meal.MealType == "00002")
                {
                    lblMT00002SetName.Text = meal.SetMealName;
                    lblMT00002SetContent.Text = meal.SetMealContent; 
                }
                else if (meal.MealType == "00003")
                {
                    lblMT00003SetName.Text = meal.SetMealName;
                    lblMT00003SetContent.Text = meal.SetMealContent; 
                }
            }
        }
    }
}
