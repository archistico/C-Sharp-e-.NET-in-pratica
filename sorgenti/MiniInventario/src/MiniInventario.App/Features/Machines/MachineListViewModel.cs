using System.Collections.ObjectModel;
using System.Windows.Input;
using MiniInventario.App.Infrastructure;
using MiniInventario.Core.Features.Machines;

namespace MiniInventario.App.Features.Machines;

public sealed class MachineListViewModel : ViewModelBase
{
    private readonly MachineService _machineService;

    private Machine? _selectedMachine;
    private Guid? _editingMachineId;

    private string _searchText = string.Empty;
    private string _name = string.Empty;
    private string _assignedUser = string.Empty;
    private string _location = string.Empty;
    private string _notes = string.Empty;
    private string _errorMessage = string.Empty;
    private string _statusMessage = string.Empty;

    public MachineListViewModel(MachineService machineService)
    {
        _machineService = machineService;

        Machines = new ObservableCollection<Machine>();

        NewCommand = new RelayCommand(NewMachine);
        SaveCommand = new RelayCommand(SaveMachine);
        DeleteCommand = new RelayCommand(DeleteMachine, CanDeleteMachine);
        RefreshCommand = new RelayCommand(RefreshMachines);

        LoadMachines();
    }

    public ObservableCollection<Machine> Machines { get; }

    public Machine? SelectedMachine
    {
        get => _selectedMachine;
        set
        {
            if (SetProperty(ref _selectedMachine, value))
            {
                LoadSelectedMachineIntoForm();
                RaiseCommandStates();
            }
        }
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                LoadMachines();
            }
        }
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string AssignedUser
    {
        get => _assignedUser;
        set => SetProperty(ref _assignedUser, value);
    }

    public string Location
    {
        get => _location;
        set => SetProperty(ref _location, value);
    }

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        private set => SetProperty(ref _errorMessage, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public ICommand NewCommand { get; }

    public ICommand SaveCommand { get; }

    public ICommand DeleteCommand { get; }

    public ICommand RefreshCommand { get; }

    public string FormTitle =>
        _editingMachineId.HasValue
            ? "Dettaglio macchina"
            : "Nuova macchina";

    private void LoadMachines(Guid? machineToSelect = null)
    {
        var result = _machineService.Search(new MachineSearchCriteria
        {
            SearchText = SearchText
        });

        Machines.Clear();

        foreach (var machine in result)
        {
            Machines.Add(machine);
        }

        if (machineToSelect.HasValue)
        {
            SelectedMachine = Machines.FirstOrDefault(machine => machine.Id == machineToSelect.Value);
        }

        RaiseCommandStates();
    }

    private void RefreshMachines()
    {
        ClearMessages();
        LoadMachines(SelectedMachine?.Id);
        StatusMessage = "Elenco aggiornato.";
    }

    private void NewMachine()
    {
        SelectedMachine = null;
        _editingMachineId = null;

        Name = string.Empty;
        AssignedUser = string.Empty;
        Location = string.Empty;
        Notes = string.Empty;

        ClearMessages();
        OnPropertyChanged(nameof(FormTitle));
        RaiseCommandStates();
    }

    private void SaveMachine()
    {
        ClearMessages();

        try
        {
            if (_editingMachineId.HasValue)
            {
                var request = new UpdateMachineRequest
                {
                    Id = _editingMachineId.Value,
                    Name = Name,
                    AssignedUser = AssignedUser,
                    Location = Location,
                    Notes = Notes
                };

                _machineService.Update(request);

                LoadMachines(_editingMachineId.Value);
                StatusMessage = "Macchina aggiornata correttamente.";
            }
            else
            {
                var request = new CreateMachineRequest
                {
                    Name = Name,
                    AssignedUser = AssignedUser,
                    Location = Location,
                    Notes = Notes
                };

                var createdMachine = _machineService.Create(request);

                LoadMachines(createdMachine.Id);
                StatusMessage = "Macchina creata correttamente.";
            }
        }
        catch (InvalidOperationException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Errore imprevisto: {ex.Message}";
        }
    }

    private bool CanDeleteMachine()
    {
        return SelectedMachine is not null && _editingMachineId.HasValue;
    }

    private void DeleteMachine()
    {
        if (SelectedMachine is null)
        {
            return;
        }

        ClearMessages();

        try
        {
            _machineService.Delete(SelectedMachine.Id);

            LoadMachines();
            NewMachine();

            StatusMessage = "Macchina eliminata correttamente.";
        }
        catch (InvalidOperationException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Errore imprevisto: {ex.Message}";
        }
    }

    private void LoadSelectedMachineIntoForm()
    {
        if (SelectedMachine is null)
        {
            return;
        }

        _editingMachineId = SelectedMachine.Id;

        Name = SelectedMachine.Name;
        AssignedUser = SelectedMachine.AssignedUser;
        Location = SelectedMachine.Location;
        Notes = SelectedMachine.Notes;

        ClearMessages();

        OnPropertyChanged(nameof(FormTitle));
    }

    private void ClearMessages()
    {
        ErrorMessage = string.Empty;
        StatusMessage = string.Empty;
    }

    private void RaiseCommandStates()
    {
        if (DeleteCommand is RelayCommand deleteCommand)
        {
            deleteCommand.RaiseCanExecuteChanged();
        }
    }
}