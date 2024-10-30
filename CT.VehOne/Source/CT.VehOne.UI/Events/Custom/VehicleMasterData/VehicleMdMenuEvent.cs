using CompuTec.Core2.UI.Attributes;
using CompuTec.Core2.UI.Events;
using CompuTec.Core2.UI.Events.Helpers;
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using CT.VehOne.UI.UI.Forms.VehicleMasterData;

namespace CT.VehOne.UI.Events.Custom.VehicleMasterData;

[EnableEvent(VehicleMasterDataForm.FormTypeEx)]
internal  sealed class VehicleMdMenuEvent:ApplicationContextMenuEvent<IVehicleMasterData>
{
    public VehicleMdMenuEvent(AppHolder appHolder, ICoreConnection coreConnection, ILogger<VehicleMdMenuEvent> logger, ITranslationService translationService, MatrixContextMenuManager matrixContextMenuManager) : base(appHolder, coreConnection, logger, translationService, matrixContextMenuManager)
    {
    }
}