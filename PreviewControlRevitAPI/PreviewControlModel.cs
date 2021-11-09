using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PreviewControlRevitAPI
{
    public class PreviewControlModel
    {
        public Document doc { get; set; }
        public FrmPreviewWindows Window { get; set; }
        public PreviewControl PreviewControl { get; set; }

        private List<View> views;
        public List<View> Views
        {
            get
            {
                return views ?? new FilteredElementCollector(doc)
                     .OfCategory(BuiltInCategory.OST_Views)
                     .WhereElementIsNotElementType()
                     .Cast<View>().Where(x => !x.IsTemplate).ToList();

            }
            set=> views = value;

        }
        public PreviewControlModel(FrmPreviewWindows frm, Document doc)
        {
            this.Window = frm;
            this.doc = doc;
            Window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.PreviewControl = new PreviewControl(doc, Views.First().Id);
            this.PreviewControl.Loaded += PreviewControlLoad;
            this.Window.GridControl.Children.Add(this.PreviewControl);
            this.Window.cbbLevel.ItemsSource = Views.OrderBy(x => x.Name);
            this.Window.cbbLevel.SelectionChanged += ChangeView;


        }

        private void ChangeView(object sender, SelectionChangedEventArgs e)
        {
            View view = this.Window.cbbLevel.SelectedItem as View;
            this.PreviewControl.Dispose();
            this.PreviewControl = new PreviewControl(doc, view.Id);
            Window.GridControl.Children.Clear();
            this.Window.GridControl.Children.Add(this.PreviewControl);
            this.PreviewControl.Loaded += PreviewControlLoad;
        }

        private void PreviewControlLoad(object sender, RoutedEventArgs e)
        {
            this.PreviewControl.UIView.ZoomToFit();
        }
    }
}
