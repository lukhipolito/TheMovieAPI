# TheMovieAPI
API com comunicação com endpoint The Movie DB (https://developers.themoviedb.org/3)

Foi feito um projeto com arquitetura simplificada baseada na Clean Architecture (https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).
No caso a solução foi separada em dois projetos:

API - Responsável pela camada de interface e ponto de entrada da API.

Core - Neste projeto encontram-se duas camadas, aplicação e enterprise. 
Onde a camada de aplicação funciona como um presenter e coordena as regras de negócio (dados contidos em 'DTOs' e 'Business', com apoio das interfaces em 'Interfaces') e a camada enterprise é onde ficam os objetos "naturais", ou seja, o seu estado padrão assim que consultado da fonte externa, no formato da fonte. Esta última pode ser vista em 'Entities'.

A camada enterprise conhece o objeto da fonte consultada, mas é a camada superior, de aplicação que conhece o objeto utilizado no projeto, então ele traduz o retorno das consultas de objetos enterprise para objetos comuns na aplicação, aplicando também as regras de negócio necessárias e devolvendo as informações tratadas para o solicitante.

O projeto também utiliza e respeita os princípios do SOLID. Onde cada objeto ou elemento da aplicação tem seu próprio papel e o contexto de sua atuação é intrínseco ao que o elemento representa. Portanto as tarefas são distribuídas de acordo com o contexto de cada objeto ou elemento, buscando ao máximo um baixo acoplamento e escalabilidade para futuras novas integrações.


Dados para consulta:
(OBS: Para este projeto estão sendo retornados dados preferencialmente em português, mas os próximos lançamentos de todo o mundo estão sendo consultados)

Método GET
https://localhost:44322/movie 

(Necessita de execução local)

retorno:
List<MovieDTO>
  onde MovieDTO:
  {
        string Titulo
        string[] Generos
        DateTime Data_Lancamento
        string Sumario
        decimal Avaliacao_Media
  }
  
(ou, em JSON, exemplo)
[
    {
        "titulo": "Força da Natureza",
        "generos": [
            "Ação",
            "Drama"
        ],
        "data_Lancamento": "2020-07-02T00:00:00",
        "sumario": "Um policial deve proteger os moradores remanescentes de um prédio no meio de uma evacuação de furacão, enquanto criminosos violentos tentam realizar um misterioso assalto dentro do prédio.",
        "avaliacao_Media": 5.4
    },
    {
        "titulo": "Strolling Through the Meadows",
        "generos": [],
        "data_Lancamento": "2020-07-01T00:00:00",
        "sumario": "",
        "avaliacao_Media": 0
    }
]
