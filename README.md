CSharp And MongoDB With BDD Learning Test
==============

CRUD operation (And File save) - Learning Test with BDD (Specflow required) - (In portuguese)

Enquanto estava aprendendo fui fazendo utilizando BDD. Por exemplo:

Cenario: Insercao
	Dado config de conexao
		| DbName | CollectionName |
		| DbTest | TestItens      |
	Quando insiro os dados
		| Id | Texto   |
		| 1  | Valor A |
		| 2  | Valor B |
	Entao devem existir os valores
		| Id | Texto   |
		| 1  | Valor A |
		| 2  | Valor B |

Ou seja, comandos básicos de CRUD e no código implementei para MongoDB.
