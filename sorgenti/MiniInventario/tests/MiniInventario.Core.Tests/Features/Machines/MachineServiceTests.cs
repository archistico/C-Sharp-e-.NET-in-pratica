using MiniInventario.Core.Features.Machines;

namespace MiniInventario.Core.Tests.Features.Machines;

public sealed class MachineServiceTests
{
    [Fact]
    public void Create_WithValidRequest_CreatesMachine()
    {
        // Arrange
        var repository = new FakeMachineRepository();
        var service = CreateService(repository);

        var request = new CreateMachineRequest
        {
            Name = "PC-AMMINISTRAZIONE-01",
            AssignedUser = "Mario Rossi",
            Location = "Ufficio Amministrazione",
            Notes = "Computer principale"
        };

        // Act
        var machine = service.Create(request);

        // Assert
        Assert.NotEqual(Guid.Empty, machine.Id);
        Assert.Equal("PC-AMMINISTRAZIONE-01", machine.Name);
        Assert.Equal("Mario Rossi", machine.AssignedUser);
        Assert.Equal("Ufficio Amministrazione", machine.Location);
        Assert.Equal("Computer principale", machine.Notes);
        Assert.True(machine.CreatedAt <= DateTime.Now);
        Assert.True(machine.UpdatedAt <= DateTime.Now);

        var machines = repository.GetAll();

        Assert.Single(machines);
        Assert.Equal(machine.Id, machines[0].Id);
    }

    [Fact]
    public void Create_WithEmptyName_ThrowsException()
    {
        // Arrange
        var repository = new FakeMachineRepository();
        var service = CreateService(repository);

        var request = new CreateMachineRequest
        {
            Name = "",
            AssignedUser = "Mario Rossi",
            Location = "Ufficio Amministrazione",
            Notes = ""
        };

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() =>
            service.Create(request));

        // Assert
        Assert.Contains("Il nome macchina è obbligatorio.", exception.Message);
        Assert.Empty(repository.GetAll());
    }

    [Fact]
    public void Create_WithEmptyLocation_ThrowsException()
    {
        // Arrange
        var repository = new FakeMachineRepository();
        var service = CreateService(repository);

        var request = new CreateMachineRequest
        {
            Name = "PC-AMMINISTRAZIONE-01",
            AssignedUser = "Mario Rossi",
            Location = "",
            Notes = ""
        };

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() =>
            service.Create(request));

        // Assert
        Assert.Contains("La posizione è obbligatoria.", exception.Message);
        Assert.Empty(repository.GetAll());
    }

    [Fact]
    public void Create_WithDuplicateName_ThrowsException()
    {
        // Arrange
        var existingMachine = CreateMachine(
            name: "PC-AMMINISTRAZIONE-01",
            assignedUser: "Mario Rossi",
            location: "Ufficio Amministrazione");

        var repository = new FakeMachineRepository(new[] { existingMachine });
        var service = CreateService(repository);

        var request = new CreateMachineRequest
        {
            Name = "pc-amministrazione-01",
            AssignedUser = "Luigi Bianchi",
            Location = "Ufficio Tecnico",
            Notes = ""
        };

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() =>
            service.Create(request));

        // Assert
        Assert.Contains("Esiste già una macchina con questo nome.", exception.Message);
        Assert.Single(repository.GetAll());
    }

    [Fact]
    public void Update_WithExistingMachine_UpdatesMachine()
    {
        // Arrange
        var machine = CreateMachine(
            name: "PC-AMMINISTRAZIONE-01",
            assignedUser: "Mario Rossi",
            location: "Ufficio Amministrazione",
            notes: "Vecchie note");

        var repository = new FakeMachineRepository(new[] { machine });
        var service = CreateService(repository);

        var request = new UpdateMachineRequest
        {
            Id = machine.Id,
            Name = "PC-AMMINISTRAZIONE-02",
            AssignedUser = "Anna Verdi",
            Location = "Ufficio Contabilità",
            Notes = "Nuove note"
        };

        // Act
        service.Update(request);

        // Assert
        var updatedMachine = repository.GetAll().Single();

        Assert.Equal(machine.Id, updatedMachine.Id);
        Assert.Equal("PC-AMMINISTRAZIONE-02", updatedMachine.Name);
        Assert.Equal("Anna Verdi", updatedMachine.AssignedUser);
        Assert.Equal("Ufficio Contabilità", updatedMachine.Location);
        Assert.Equal("Nuove note", updatedMachine.Notes);
        Assert.Equal(machine.CreatedAt, updatedMachine.CreatedAt);
        Assert.True(updatedMachine.UpdatedAt >= machine.UpdatedAt);
    }

    [Fact]
    public void Update_WithMissingMachine_ThrowsException()
    {
        // Arrange
        var repository = new FakeMachineRepository();
        var service = CreateService(repository);

        var request = new UpdateMachineRequest
        {
            Id = Guid.NewGuid(),
            Name = "PC-AMMINISTRAZIONE-01",
            AssignedUser = "Mario Rossi",
            Location = "Ufficio Amministrazione",
            Notes = ""
        };

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() =>
            service.Update(request));

        // Assert
        Assert.Contains("La macchina da modificare non esiste.", exception.Message);
    }

    [Fact]
    public void Update_WithDuplicateName_ThrowsException()
    {
        // Arrange
        var firstMachine = CreateMachine(
            name: "PC-AMMINISTRAZIONE-01",
            assignedUser: "Mario Rossi",
            location: "Ufficio Amministrazione");

        var secondMachine = CreateMachine(
            name: "PC-TECNICO-01",
            assignedUser: "Luca Bianchi",
            location: "Ufficio Tecnico");

        var repository = new FakeMachineRepository(new[] { firstMachine, secondMachine });
        var service = CreateService(repository);

        var request = new UpdateMachineRequest
        {
            Id = secondMachine.Id,
            Name = "pc-amministrazione-01",
            AssignedUser = "Luca Bianchi",
            Location = "Ufficio Tecnico",
            Notes = ""
        };

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() =>
            service.Update(request));

        // Assert
        Assert.Contains("Esiste già una macchina con questo nome.", exception.Message);
    }

    [Fact]
    public void Delete_WithExistingMachine_RemovesMachine()
    {
        // Arrange
        var machine = CreateMachine(
            name: "PC-AMMINISTRAZIONE-01",
            assignedUser: "Mario Rossi",
            location: "Ufficio Amministrazione");

        var repository = new FakeMachineRepository(new[] { machine });
        var service = CreateService(repository);

        // Act
        service.Delete(machine.Id);

        // Assert
        Assert.Empty(repository.GetAll());
    }

    [Fact]
    public void Delete_WithMissingMachine_ThrowsException()
    {
        // Arrange
        var repository = new FakeMachineRepository();
        var service = CreateService(repository);

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() =>
            service.Delete(Guid.NewGuid()));

        // Assert
        Assert.Contains("La macchina da eliminare non esiste.", exception.Message);
    }

    [Fact]
    public void Search_ByName_ReturnsMatchingMachines()
    {
        // Arrange
        var machines = new[]
        {
            CreateMachine("PC-AMMINISTRAZIONE-01", "Mario Rossi", "Ufficio Amministrazione"),
            CreateMachine("PC-TECNICO-01", "Luca Bianchi", "Ufficio Tecnico")
        };

        var repository = new FakeMachineRepository(machines);
        var service = CreateService(repository);

        // Act
        var result = service.Search(new MachineSearchCriteria
        {
            SearchText = "amministrazione"
        });

        // Assert
        var machine = Assert.Single(result);
        Assert.Equal("PC-AMMINISTRAZIONE-01", machine.Name);
    }

    [Fact]
    public void Search_ByAssignedUser_ReturnsMatchingMachines()
    {
        // Arrange
        var machines = new[]
        {
            CreateMachine("PC-AMMINISTRAZIONE-01", "Mario Rossi", "Ufficio Amministrazione"),
            CreateMachine("PC-TECNICO-01", "Luca Bianchi", "Ufficio Tecnico")
        };

        var repository = new FakeMachineRepository(machines);
        var service = CreateService(repository);

        // Act
        var result = service.Search(new MachineSearchCriteria
        {
            SearchText = "luca"
        });

        // Assert
        var machine = Assert.Single(result);
        Assert.Equal("PC-TECNICO-01", machine.Name);
    }

    [Fact]
    public void Search_ByLocation_ReturnsMatchingMachines()
    {
        // Arrange
        var machines = new[]
        {
            CreateMachine("PC-AMMINISTRAZIONE-01", "Mario Rossi", "Ufficio Amministrazione"),
            CreateMachine("PC-TECNICO-01", "Luca Bianchi", "Ufficio Tecnico")
        };

        var repository = new FakeMachineRepository(machines);
        var service = CreateService(repository);

        // Act
        var result = service.Search(new MachineSearchCriteria
        {
            SearchText = "tecnico"
        });

        // Assert
        var machine = Assert.Single(result);
        Assert.Equal("PC-TECNICO-01", machine.Name);
    }

    [Fact]
    public void Search_WithEmptyText_ReturnsAllMachinesOrderedByName()
    {
        // Arrange
        var machines = new[]
        {
            CreateMachine("PC-TECNICO-01", "Luca Bianchi", "Ufficio Tecnico"),
            CreateMachine("PC-AMMINISTRAZIONE-01", "Mario Rossi", "Ufficio Amministrazione")
        };

        var repository = new FakeMachineRepository(machines);
        var service = CreateService(repository);

        // Act
        var result = service.Search(new MachineSearchCriteria
        {
            SearchText = ""
        });

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("PC-AMMINISTRAZIONE-01", result[0].Name);
        Assert.Equal("PC-TECNICO-01", result[1].Name);
    }

    private static MachineService CreateService(FakeMachineRepository repository)
    {
        return new MachineService(
            repository,
            new MachineValidator());
    }

    private static Machine CreateMachine(
        string name,
        string assignedUser,
        string location,
        string notes = "")
    {
        var now = DateTime.Now;

        return new Machine
        {
            Id = Guid.NewGuid(),
            Name = name,
            AssignedUser = assignedUser,
            Location = location,
            Notes = notes,
            CreatedAt = now,
            UpdatedAt = now
        };
    }
}