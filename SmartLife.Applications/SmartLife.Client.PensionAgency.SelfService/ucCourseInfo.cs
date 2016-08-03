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
    public partial class ucCourseInfo : UserControl
    {
        public ucCourseInfo()
        {
            InitializeComponent();
        }

        public void LoadCourseInfo()
        {
            lbAM.Items.Clear();
            lbPM.Items.Clear();
            var amCourses = Data.Courses.Where(item => item.AMPM == "AM");
            var pmCourses = Data.Courses.Where(item => item.AMPM == "PM");

            foreach (var course in amCourses)
            {
                lbAM.Items.Add("   " + course.BeginTime + " " + course.CourseName);
            }

            foreach (var course in pmCourses)
            {
                lbPM.Items.Add("   " + course.BeginTime + " " + course.CourseName);
            }
        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush myBrush = Brushes.Black;

            using (Graphics g = e.Graphics)
            {
                //如果该项被选择 
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds);
                else
                    e.Graphics.FillRectangle(new SolidBrush((sender as ListBox).BackColor), e.Bounds);
                //画出项文本
                e.Graphics.DrawString((sender as ListBox).Items[e.Index].ToString(), e.Font, myBrush, e.Bounds.X, e.Bounds.Y);
                e.DrawFocusRectangle();
            } 
        } 
    }
}
