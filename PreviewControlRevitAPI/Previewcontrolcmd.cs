using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PreviewControlRevitAPI
{
    [Transaction(TransactionMode.Manual)]
    public class Previewcontrolcmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            FilteredElementCollector collecotr = new FilteredElementCollector(doc);
            collecotr.OfClass(typeof(Autodesk.Revit.DB.View));

            FrmPreviewWindows win = new FrmPreviewWindows();
            PreviewControl previewControl = new PreviewControl(doc, doc.ActiveView.Id);
            previewControl.Loaded += (sender, args) =>
            {
                previewControl.UIView.ZoomToFit();
            };
            win.grid1.Children.Add(previewControl);
            
            win.ShowDialog();
            return Result.Succeeded;
        }
    }
}
