using CompuTec.Core2.Beans;
using CompuTec.Core2.UI.Attributes;
using CompuTec.Core2.UI.Events;
using CompuTec.Core2.UI.Events.Helpers;
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using CT.VehOne.UI.UI.Forms.VehicleMasterData;
using SAPbouiCOM;

namespace CT.VehOne.UI.Events.Custom.VehicleMasterData;

[EnableEvent(VehicleMasterDataForm.FormTypeEx)]
public class VehicleMDContextMenuIntitializer:ApplicationRightClickEvent<IVehicleMasterData>
{
    public VehicleMDContextMenuIntitializer(AppHolder appHolder, ICoreConnection coreConnection, ILogger<VehicleMDContextMenuIntitializer> logger, ITranslationService translationService, ContextMenuManager menuManager) : base(appHolder, coreConnection, logger, translationService, menuManager)
    {
        _contextMenuManager.Add(
            new(VehicleMasterDataForm.MenuIods.ContextMenu1,"CT_VO_ContextMn1Desctiption",ContextMenuType.String,CanDuplicate));
    }
    private bool CanDuplicate(Form form, ref IUDOBean udo, PContextMenuInfo eventInfo)
    {
        return form.Mode.In(BoFormMode.fm_OK_MODE, BoFormMode.fm_UPDATE_MODE, BoFormMode.fm_VIEW_MODE);
    }
}