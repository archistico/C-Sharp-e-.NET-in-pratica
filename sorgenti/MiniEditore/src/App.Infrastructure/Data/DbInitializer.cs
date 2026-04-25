using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using App.Domain.Catalogo;
using App.Domain.Clienti;
using App.Domain.Documenti;

namespace App.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext dbContext)
	{
		string? dataSource = dbContext.Database.GetConnectionString();
	
		if (!string.IsNullOrWhiteSpace(dataSource))
		{
			SqliteConnectionStringBuilder builder = new(dataSource);
	
			if (!string.IsNullOrWhiteSpace(builder.DataSource)
				&& builder.DataSource != ":memory:")
			{
				string? directory = Path.GetDirectoryName(builder.DataSource);
	
				if (!string.IsNullOrWhiteSpace(directory))
				{
					Directory.CreateDirectory(directory);
				}
			}
		}
	
		await dbContext.Database.EnsureCreatedAsync();
	
		if (await dbContext.Libri.AnyAsync())
		{
			return;
		}

        if (await dbContext.Libri.AnyAsync()) return;

        // Collane
        var coll1 = new Collana("Narrativa contemporanea", null);
        var coll2 = new Collana("Saggi brevi", null);
        var coll3 = new Collana("Manuali tecnici", null);

        await dbContext.Collane.AddRangeAsync(coll1, coll2, coll3);

        // Autori
        var a1 = new Autore("Italo", "Calvino");
        var a2 = new Autore("Natalia", "Ginzburg");
        var a3 = new Autore("Ada", "Lovelace");
        var a4 = new Autore("Martin", "Fowler");

        await dbContext.Autori.AddRangeAsync(a1, a2, a3, a4);

        // Clienti
        var c1 = new Cliente("Libreria Centrale", "info@libreriacentrale.example");
        var c2 = new Cliente("Biblioteca Comunale", "info@biblioteca.example");
        var c3 = new Cliente("Studio Editoriale Rossi", "contatti@editorialerossi.example");

        await dbContext.Clienti.AddRangeAsync(c1, c2, c3);

        await dbContext.SaveChangesAsync();

        // Libri
        var l1 = new Libro("Il sentiero dei nidi di ragno", null, 12.50m, DateOnly.FromDateTime(new DateTime(1947,1,1)), coll1.Id);
        var l2 = new Libro("Lessico famigliare", null, 15.00m, DateOnly.FromDateTime(new DateTime(1963,1,1)), coll1.Id);
        var l3 = new Libro("Introduzione a C#", null, 39.90m, null, coll3.Id);
        var l4 = new Libro("Architetture software pragmatiche", null, 49.90m, null, coll3.Id);

        await dbContext.Libri.AddRangeAsync(l1, l2, l3, l4);
        await dbContext.SaveChangesAsync();

        // Imposta autori dopo che libri/autori hanno id
        l1.ImpostaAutori(new[] { a1.Id });
        l2.ImpostaAutori(new[] { a2.Id });
        l3.ImpostaAutori(new[] { a3.Id });
        l4.ImpostaAutori(new[] { a4.Id });

        await dbContext.SaveChangesAsync();

        // Documenti
        var doc1 = new Documento("DOC-001", DateOnly.FromDateTime(DateTime.Today.AddDays(-10)), c1.Id, TipoDocumento.Vendita);
        doc1.AggiungiRiga(new DocumentoRiga(l1.Id, l1.Titolo, 2, l1.Prezzo, 0));
        doc1.AggiungiRiga(new DocumentoRiga(l3.Id, l3.Titolo, 1, l3.Prezzo, 10));
        doc1.Conferma();

        var doc2 = new Documento("DOC-002", DateOnly.FromDateTime(DateTime.Today.AddDays(-5)), c2.Id, TipoDocumento.Vendita);
        doc2.AggiungiRiga(new DocumentoRiga(l2.Id, l2.Titolo, 3, l2.Prezzo, 0));
        doc2.Conferma();

        var doc3 = new Documento("DOC-003", DateOnly.FromDateTime(DateTime.Today), c3.Id, TipoDocumento.Ordine);
        doc3.AggiungiRiga(new DocumentoRiga(l4.Id, l4.Titolo, 1, l4.Prezzo, 5));
        // leave as bozza

        await dbContext.Documenti.AddRangeAsync(doc1, doc2, doc3);
        await dbContext.SaveChangesAsync();
    }
}
