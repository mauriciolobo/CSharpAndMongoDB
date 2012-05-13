#language: pt-BR
Funcionalidade: CRUD Mongo DB
	Digamos que eu sou um completo estupido
	E preciso fazer um CRUD
	Mas utilizando o MongoDB

Cenário: Inserção
	Dado configuração de conexão:
		| DbName | CollectionName |
		| DbTest | TestItens      |
	Quando insiro os dados
		| Id | Texto   |
		| 1  | Valor A |
		| 2  | Valor B |
	Então devem existir os valores
		| Id | Texto   |
		| 1  | Valor A |
		| 2  | Valor B |

Cenário: Atualizar
	Dado configuração de conexão:
		| DbName | CollectionName |
		| DbTest | TestItens      |
		E existe o dado
			| Id | Texto   |
			| 1  | Valor A |
	Quando troco para o valor
		| Id | Texto   | TextoNovo   |
		| 1  | Valor A | Novo Valor A |
	Então novo valor deve ser
		| Id | Texto   |
		| 1  | Novo Valor A |

Cenário: Deletar
	Dado configuração de conexão:
		| DbName | CollectionName |
		| DbTest | TestItens      |
		E existe o dado
			| Id | Texto   |
			| 1  | Valor A |
	Quando apago
		| Id |
		| 1  |
	Então o valor não deve existir
		| Id |
		| 1  |

Cenário: InserirUmArquivo
	Dado configuração de conexão:
		| DbName | CollectionName |
		| DbTest | TestItens      |
		E um arquivo
	Quando salvo no banco com nome "Teste.txt"
	Então o arquivo "Teste.txt" deve estar armazenado