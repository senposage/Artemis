using System.Threading.Tasks;
using Artemis.Core.Services;
using Artemis.Storage.Entities.Surface;
using Artemis.UI.Shared.Services;

namespace Artemis.UI.Screens.Settings;

public sealed class StoredDeviceSettingsViewModel
{
    private readonly IDeviceService _deviceService;
    private readonly IWindowService _windowService;

    public StoredDeviceSettingsViewModel(DeviceEntity deviceEntity, IDeviceService deviceService, IWindowService windowService)
    {
        DeviceEntity = deviceEntity;
        _deviceService = deviceService;
        _windowService = windowService;
    }

    public DeviceEntity DeviceEntity { get; }
    public string Identifier => DeviceEntity.Id;
    public string DisplayName => string.IsNullOrWhiteSpace(DeviceEntity.DisplayName) ? "Saved device (name unavailable)" : DeviceEntity.DisplayName;
    public string IdentityHint => Identifier.Length <= 96 ? Identifier : $"{Identifier[..48]}…{Identifier[^32..]}";

    public async Task ForgetDevice()
    {
        bool confirmed = await _windowService.ShowConfirmContentDialog(
            "Remove missing device",
            $"Permanently remove {DisplayName} ({Identifier})? Its saved settings and all layer bindings will be deleted. This cannot be undone.",
            "Remove",
            "Cancel");
        if (confirmed)
            _deviceService.ForgetDevice(DeviceEntity);
    }
}
