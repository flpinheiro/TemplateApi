# TemplateApi

Template api example

## Frontend

[Frontend](https://github.com/flpinheiro/TemplateApi.Frontend)

## Usefull Links

* [Regex 101](https://regex101.com/)
Regex Checcker
* [4 Devs](https://www.4devs.com.br/)
Useful BrazilianGenerator (CPF, CNPJ, Addres, Person, company, etc)
* [Vs code rest client extension](https://github.com/Huachao/vscode-restclient)

## dotnet tools used

* [Entity Framework tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
Entity Framework Core tools reference - .NET Core CLI
* [Versionize](https://github.com/versionize/versionize)
Automatic versioning and CHANGELOG generation, using [conventional commit messages](https://conventionalcommits.org/).


## Entity Framework Core tools

The command-line interface (CLI) tools for Entity Framework Core perform design-time development tasks. For example, they create migrations, apply migrations, and generate code for a model based on an existing database. The commands are an extension to the cross-platform dotnet command, which is part of the .NET Core SDK. These tools work with .NET Core projects.

### Instalation

```
dotnet tool install --global dotnet-ef
```

### dotnet ef migration scripts

```
dotnet ef migrations script
```
|Argument |	Description |
|-|-|
|`<FROM>` | The starting migration. Migrations may be identified by name or by ID. The number 0 is a special case that means before the first migration. Defaults to 0.| 
|`<TO>`|	The ending migration. Defaults to the last migration.|

Options:

| Option |	Short |	Description |
|-|-|-|
|--output  `<FILE>`|	-o	| The file to write the script to.| 
| --idempotent| 	-i| 	Generate a script that can be used on a database at any migration.| 
| --no-transactions	|  |	Don't generate SQL transaction statements. Added in EF Core 5.0.| 

## Versionize

### Installation

```bash
dotnet tool install --global Versionize
```

### Usage

```bash
Usage: versionize [command] [options]

Options:
  -?|-h|--help                         Show help information.
  -v|--version                         Show version information.
  -w|--workingDir <WORKING_DIRECTORY>  Directory containing projects to version
  -d|--dry-run                         Skip changing versions in projects, changelog generation and git commit
  --skip-dirty                         Skip git dirty check
  -r|--release-as <VERSION>            Specify the release version manually
  --silent                             Suppress output to console
  --skip-commit                        Skip commit and git tag after updating changelog and incrementing the
                                       version
  -i|--ignore-insignificant-commits    Do not bump the version if no significant commits (fix, feat or BREAKING)
                                       are found
  --exit-insignificant-commits         Exits with a non zero exit code if no significant commits (fix, feat or
                                       BREAKING) are found
  --changelog-all                      Include all commits in the changelog not just fix, feat and breaking changes
  --commit-suffix                      Suffix to be added to the end of the release commit message (e.g. [skip ci])
  -p|--pre-release                     Release as pre-release version with given pre release label.
  -a|--aggregate-pre-releases          Include all pre-release commits in the changelog since the last full version.

Commands:
  inspect                              Prints the current version to stdout
```

### Supported commit types

Every commit should be in the form
`<type>[optional scope]: <description>`
for example
`fix(parser): remove colon from type and scope`

* fix - will trigger a patch version increment in the next release
* feat - will trigger a minor version increment in the next release
* all other types - you can use any commit type but that commit type will not trigger a version increment in the next release

Breaking changes **must** contain a line prefixed with `BREAKING CHANGE:` to allow versionize recognizing a breaking change. Breaking changes can use any commit type.

**Example**

```bash
git commit -m "chore: update dependencies" -m "BREAKING CHANGE: this will likely break the interface"
```

## Conventional Commit Messages

The Conventional Commits specification is a lightweight convention on top of commit messages. It provides an easy set of rules for creating an explicit commit history; which makes it easier to write automated tools on top of. This convention dovetails with SemVer, by describing the features, fixes, and breaking changes made in commit messages.

The commit message should be structured as follows:

```
    <type>[optional scope]: <description>

    [optional body]

    [optional footer(s)]
```


The commit contains the following structural elements, to communicate intent to the consumers of your library:

1. **fix**: a commit of the type fix patches a bug in your codebase (this correlates with PATCH in Semantic Versioning).
2. **feat**: a commit of the type feat introduces a new feature to the codebase (this correlates with MINOR in Semantic Versioning).
3. **BREAKING CHANGE**: a commit that has a footer BREAKING CHANGE:, or appends a **!** after the type/scope, introduces a breaking API change (correlating with **MAJOR** in Semantic Versioning). A BREAKING CHANGE can be part of commits of any type.\
4. types other than fix: and feat: are allowed, for example @commitlint/config-conventional (based on the the Angular convention) recommends build:, chore:, ci:, docs:, style:, refactor:, perf:, test:, and others.
5. footers other than BREAKING CHANGE: `<description>` may be provided and follow a convention similar to git trailer format.
Additional types are not mandated by the Conventional Commits specification, and have no implicit effect in Semantic Versioning (unless they include a BREAKING CHANGE). A scope may be provided to a commit’s type, to provide additional contextual information and is contained within parenthesis, e.g., feat(parser): add ability to parse arrays.

### Examples

#### Commit message with description and breaking change footer
```
feat: allow provided config object to extend other configs
BREAKING CHANGE: `extends` key in config file is now used for extending other config files
```
#### Commit message with ! to draw attention to breaking change
```
feat!: send an email to the customer when a product is shipped
```
#### Commit message with scope and ! to draw attention to breaking change
```
feat(api)!: send an email to the customer when a product is shipped
```
#### Commit message with both ! and BREAKING CHANGE footer
```
chore!: drop support for Node 6

BREAKING CHANGE: use JavaScript features not available in Node 6.
```
#### Commit message with no body
```
docs: correct spelling of CHANGELOG
```
#### Commit message with scope
```
feat(lang): add Polish language
```
#### Commit message with multi-paragraph body and multiple footers
```
fix: prevent racing of requests


Introduce a request id and a reference to latest request. Dismiss
incoming responses other than from latest request.

Remove timeouts which were used to mitigate the racing issue but are
obsolete now.

Reviewed-by: Z
Refs: #123
```

### Specification

The key words “MUST”, “MUST NOT”, “REQUIRED”, “SHALL”, “SHALL NOT”, “SHOULD”, “SHOULD NOT”, “RECOMMENDED”, “MAY”, and “OPTIONAL” in this document are to be interpreted as described in RFC 2119.

1. Commits MUST be prefixed with a type, which consists of a noun, feat, fix, etc., followed by the OPTIONAL scope, OPTIONAL !, and REQUIRED terminal colon and space.
2. The type feat MUST be used when a commit adds a new feature to your application or library.
3. The type fix MUST be used when a commit represents a bug fix for your application.
4. A scope MAY be provided after a type. A scope MUST consist of a noun describing a section of the codebase surrounded by parenthesis, e.g., fix(parser):
5. A description MUST immediately follow the colon and space after the type/scope prefix. The description is a short summary of the code changes, e.g., fix: array parsing issue when multiple spaces were contained in string.
6. A longer commit body MAY be provided after the short description, providing additional contextual information about the code changes. The body MUST begin one blank line after the description.
7. A commit body is free-form and MAY consist of any number of newline separated paragraphs.
8. One or more footers MAY be provided one blank line after the body. Each footer MUST consist of a word token, followed by either a :<space> or <space># separator, followed by a string value (this is inspired by the git trailer convention).
9. A footer’s token MUST use - in place of whitespace characters, e.g., Acked-by (this helps differentiate the footer section from a multi-paragraph body). An exception is made for BREAKING CHANGE, which MAY also be used as a token.
10. A footer’s value MAY contain spaces and newlines, and parsing MUST terminate when the next valid footer token/separator pair is observed.
11. Breaking changes MUST be indicated in the type/scope prefix of a commit, or as an entry in the footer.
12. If included as a footer, a breaking change MUST consist of the uppercase text BREAKING CHANGE, followed by a colon, space, and description, e.g., BREAKING CHANGE: environment variables now take precedence over config files.
13. If included in the type/scope prefix, breaking changes MUST be indicated by a ! immediately before the :. If ! is used, BREAKING CHANGE: MAY be omitted from the footer section, and the commit description SHALL be used to describe the breaking change.
14. Types other than feat and fix MAY be used in your commit messages, e.g., docs: updated ref docs.
15. The units of information that make up Conventional Commits MUST NOT be treated as case sensitive by implementors, with the exception of BREAKING CHANGE which MUST be uppercase.
16. BREAKING-CHANGE MUST be synonymous with BREAKING CHANGE, when used as a token in a footer.
