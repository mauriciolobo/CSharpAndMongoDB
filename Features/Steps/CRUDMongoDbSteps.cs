using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace Zona.Mongolizar.Features.Steps
{
    [Binding]
    public class CRUDMongoDbSteps
    {
        private MongoCollection<BsonDocument> collection;
        private byte[] arquivo;

        [Given(@"configuração de conexão:")]
        public void DadoConfiguracaoDeConexao(Table table)
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var server = MongoServer.Create(connectionString);
            var database = server.GetDatabase(table.Rows[0]["DbName"]);
            collection = database.GetCollection(table.Rows[0]["CollectionName"]);
        }

        [Given(@"existe o dado")]
        public void DadoExisteODado(Table table)
        {
            InserirDadoNoDB(table);
        }

        [Given(@"um arquivo")]
        public void DadoUmArquivo()
        {
            arquivo = Encoding.UTF8.GetBytes("Isso é um teste");
        }

        [When(@"insiro os dados")]
        public void QuandoInsiroOsDados(Table table)
        {
            InserirDadoNoDB(table);
        }

        private void InserirDadoNoDB(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                collection.Insert(new Entidade { Id = tableRow["Id"], Texto = tableRow["Texto"] });
            }
        }

        [When(@"troco para o valor")]
        public void QuandoTrocoParaOValor(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                var query = Query.EQ("_id", tableRow["Id"]);
                var updateValue = Update.Set("Texto", tableRow["TextoNovo"]);

                collection.Update(query, updateValue);
            }
        }

        [When(@"apago")]
        public void QuandoApago(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                var query = Query.EQ("_id", tableRow["Id"]);
                collection.Remove(query);
            }
        }

        [When(@"salvo no banco com nome ""(.*)""")]
        public void QuandoSalvoNoBancoComNome(string nomeArquivo)
        {
            var entidade = new Entidade { Id = 1, NomeArquivo = nomeArquivo, Arquivo = arquivo };
            collection.Save(entidade);

        }

        [Then(@"devem existir os valores")]
        public void EntaoDevemExistirOsValores(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                var query = Query.EQ("_id", tableRow["Id"]);
                var entity = collection.FindOne(query);

                entity.IsBsonNull.Should().BeFalse();
                tableRow["Texto"].Should().Be(entity["Texto"].AsString);
            }
        }

        [Then(@"novo valor deve ser")]
        public void EntaoNovoValorDeveSer(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                var query = Query.EQ("_id", tableRow["Id"]);
                collection.FindOne(query)["Texto"].Should().Be(tableRow["Texto"]);
            }
        }

        [Then(@"o valor não deve existir")]
        public void EntaoOValorNaoDeveExistir(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                var query = Query.EQ("_id", tableRow["Id"]);
                collection.Find(query).Count().Should().BeLessOrEqualTo(0);
            }
        }

        [Then(@"o arquivo ""(.*)"" deve estar armazenado")]
        public void EntaoOArquivoDeveEstarArmazenado(string nomeArquivo)
        {
            var query = Query.EQ("NomeArquivo", nomeArquivo);
            
            var entity = collection.FindOne(query);
            
            entity.IsBsonNull.Should().BeFalse();
            entity["Arquivo"].AsByteArray.Should().BeEquivalentTo(arquivo);
        }


        [AfterScenario()]
        public void WhenFinishes()
        {
            collection.Drop();
        }
    }
}
