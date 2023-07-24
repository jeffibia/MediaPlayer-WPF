# Media Player usando WPF

### Instruções de uso:

#### Compilar:
Esse projeto foi desenvolvido usando visual studio community 2022. Para compilar-lo abra o visual studio community, clique em <b>Compilação -> Compilar Solução</b>

#### Rodando:
Usando o visual studio community, clique em <b>Depurar -> Iniciar Depuração.</b>
Ou vá até a pasta bin/Debug/net6.0-windows e execute o arquivo.

Pelo VSCode, vá até a pasta onde está o *.csproj e digite ```dotnet run.```

#### Arquitetura
Para o desenvolvimento da aplicação utilizamos o WPF com o pattern de MVVM (Model view viewmodel). A Aplicação possui 2 classes no model:

#### Model
> MediaItem: Classe que representa uma mídia do sistema e possui um identidicador único, um titulo e um caminho onde o arquivo está no disco.

> PlayListHandle: Classe que cuida do acesso aos arquivos e retorna a lista de arquivos/pastas selecionados para confecção da playlist.

#### ViewModel

> PlaylistViewModel: Classe de interface entre a view e a model. Nela temos o bind para nossas classes na model.


