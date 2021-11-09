using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;

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
            FrmPreviewWindows frm = new FrmPreviewWindows();
            PreviewControlModel control = new PreviewControlModel(frm, doc);
            control.Window.ShowDialog();
            return Result.Succeeded;
        }


    }

   
}
