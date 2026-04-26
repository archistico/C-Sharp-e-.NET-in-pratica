using System.IO;
using System.Windows;
using MiniInventario.App.Features.Machines;
using MiniInventario.App.Infrastructure;
using MiniInventario.Core.Features.Machines;
using MiniInventario.Core.Infrastructure.Storage;

namespace MiniInventario.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var appDataDirectory = Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData);

        var dataDirectory = Path.Combine(
            appDataDirectory,
            "MiniInventario");

        var jsonFilePath = Path.Combine(
            dataDirectory,
            "machines.json");

        var storage = new JsonMachineStorage(jsonFilePath);
        var repository = new MachineRepository(storage);
        var validator = new MachineValidator();
        var service = new MachineService(repository, validator);
        var messageService = new MessageService();

        MachineList.DataContext = new MachineListViewModel(
            service,
            messageService);
    }
}