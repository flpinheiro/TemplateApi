# Unit Test

## installed Packs 

* [Moq](https://www.nuget.org/packages/Moq)
* [coverlet.collector](https://www.nuget.org/packages/coverlet.collector/)
* [coverlet.msbuild](https://www.nuget.org/packages/coverlet.msbuild/)

## Cobertura de Testes com Coverlet

O Coverlet é uma biblioteca open source concebida com o objetivo de medir a cobertura de código em projetos .NET. Podemos configurar a execução de testes para que gere um arquivo no formato XML contendo os indicadores de cobertura; isto foi exemplificado no comando a seguir:

		dotnet test --verbosity minimal --collect:"XPlat Code Coverage"

Em que o uso da opção --collect:”XPlat Code Coverage” procederá com a criação do arquivo coverage.cobertura.xml (em uma pasta com o nome identificado por um GUID e localizada dentro diretório TestResults do projeto de testes):

Há uma Global Tool chamada ReportGenerator, que pode ser instalada através do comando a seguir:

		dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.8.6

Acessando o diretório em que se encontra o XML com o resultado do Coverlet será executado então o comando:

		reportgenerator "-reports:TemplateApi.UniTest/coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html

Como resultado teremos a geração do diretório coveragereport contendo um site estático:

Acessando o arquivo index.html aparecerá um sumário com a cobertura de testes:

Sendo possível inclusive obter uma visualização do que foi ou não analisado em uma classe que passou por validação:

Todo este processo que culmina com a geração do site estático com a cobertura de testes pode ser automatizado, conforme exemplificado no seguinte workflow do GitHub Actions:

## Tools

E quanto ao Visual Studio 2019, podemos executar tudo isso de maneira mais simples e valendo-se do poder desta IDE?

A resposta a esta questão está na extensão gratuita Run Coverlet Report:

Após a instalação desta extensão aparecerão no menu Tools as opções:

		Run Code Coverage
		
		Toggle Code Coverage Highlighting

Ao acionar a opção Run Code Coverage o relatório de cobertura de testes será gerado e exibido dentro do Visual Studio 2019:

Já ao manter ativada a opção Toggle Code Coverage Highlighting trechos de código validados serão destacados em verde, ao passo que linhas de código não testadas aparecerão em vermelho:

## cli

		dotnet test -s .\CodeCoverage.runsettings  --blame --blame-crash -c Debug --verbosity minimal --collect:"XPlat Code Coverage"


## References

* [.NET 5 + Cobertura de Testes com Coverlet](https://renatogroffe.medium.com/net-5-cobertura-de-testes-com-coverlet-7cbec2f052d9)