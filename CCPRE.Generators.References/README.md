<a name='assembly'></a>
# CCPRE.Generators.References

## Contents

- [CopilotReferenceGeneratorBase](#T-CCPRE-Generators-References-CopilotReferenceGeneratorBase 'CCPRE.Generators.References.CopilotReferenceGeneratorBase')
  - [#ctor()](#M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-#ctor 'CCPRE.Generators.References.CopilotReferenceGeneratorBase.#ctor')
  - [DirectorySeparatorChar](#P-CCPRE-Generators-References-CopilotReferenceGeneratorBase-DirectorySeparatorChar 'CCPRE.Generators.References.CopilotReferenceGeneratorBase.DirectorySeparatorChar')
  - [GeneratorType](#P-CCPRE-Generators-References-CopilotReferenceGeneratorBase-GeneratorType 'CCPRE.Generators.References.CopilotReferenceGeneratorBase.GeneratorType')
  - [#cctor()](#M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-#cctor 'CCPRE.Generators.References.CopilotReferenceGeneratorBase.#cctor')
  - [AppendDirectorySeparator(path)](#M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-AppendDirectorySeparator-System-String- 'CCPRE.Generators.References.CopilotReferenceGeneratorBase.AppendDirectorySeparator(System.String)')
  - [Generate(selectedObject,solutionDirectory)](#M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-Generate-System-Object,System-String- 'CCPRE.Generators.References.CopilotReferenceGeneratorBase.Generate(System.Object,System.String)')
  - [OnGenerate(selectedObject,solutionDirectory)](#M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-OnGenerate-System-Object,System-String- 'CCPRE.Generators.References.CopilotReferenceGeneratorBase.OnGenerate(System.Object,System.String)')
- [FileCopilotReferenceGenerator](#T-CCPRE-Generators-References-FileCopilotReferenceGenerator 'CCPRE.Generators.References.FileCopilotReferenceGenerator')
  - [#ctor()](#M-CCPRE-Generators-References-FileCopilotReferenceGenerator-#ctor 'CCPRE.Generators.References.FileCopilotReferenceGenerator.#ctor')
  - [GeneratorType](#P-CCPRE-Generators-References-FileCopilotReferenceGenerator-GeneratorType 'CCPRE.Generators.References.FileCopilotReferenceGenerator.GeneratorType')
  - [Instance](#P-CCPRE-Generators-References-FileCopilotReferenceGenerator-Instance 'CCPRE.Generators.References.FileCopilotReferenceGenerator.Instance')
  - [#cctor()](#M-CCPRE-Generators-References-FileCopilotReferenceGenerator-#cctor 'CCPRE.Generators.References.FileCopilotReferenceGenerator.#cctor')
  - [NormalizeSeparators(relativePath)](#M-CCPRE-Generators-References-FileCopilotReferenceGenerator-NormalizeSeparators-System-String- 'CCPRE.Generators.References.FileCopilotReferenceGenerator.NormalizeSeparators(System.String)')
  - [OnGenerate(selectedObject,solutionDirectory)](#M-CCPRE-Generators-References-FileCopilotReferenceGenerator-OnGenerate-System-Object,System-String- 'CCPRE.Generators.References.FileCopilotReferenceGenerator.OnGenerate(System.Object,System.String)')
  - [ToSolutionRelative(absolutePath,solutionDirectory)](#M-CCPRE-Generators-References-FileCopilotReferenceGenerator-ToSolutionRelative-System-String,System-String- 'CCPRE.Generators.References.FileCopilotReferenceGenerator.ToSolutionRelative(System.String,System.String)')
- [Resources](#T-CCPRE-Generators-References-Properties-Resources 'CCPRE.Generators.References.Properties.Resources')
  - [Culture](#P-CCPRE-Generators-References-Properties-Resources-Culture 'CCPRE.Generators.References.Properties.Resources.Culture')
  - [ResourceManager](#P-CCPRE-Generators-References-Properties-Resources-ResourceManager 'CCPRE.Generators.References.Properties.Resources.ResourceManager')

<a name='T-CCPRE-Generators-References-CopilotReferenceGeneratorBase'></a>
## CopilotReferenceGeneratorBase `type`

##### Namespace

CCPRE.Generators.References

##### Summary

Provides a base implementation of
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
using the Template Method pattern.

##### Remarks

This abstract class provides common functionality for all Copilot reference
generator strategy(ies), including input validation and error handling.



Derived class(es) must override the
[OnGenerate](#M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-OnGenerate-System-Object,System-String- 'CCPRE.Generators.References.CopilotReferenceGeneratorBase.OnGenerate(System.Object,System.String)')
method to provide type-specific reference generation logic.

<a name='M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of
[CopilotReferenceGeneratorBase](#T-CCPRE-Generators-References-CopilotReferenceGeneratorBase 'CCPRE.Generators.References.CopilotReferenceGeneratorBase') and
returns a reference to it.

##### Parameters

This constructor has no parameters.

##### Remarks

This constructor is marked
`protected` due to the fact that this class is marked
`abstract`.



We've decorated this constructor with the `[Log(AttributeExclude = true)]`
attribute in order to simplify the logging output.

<a name='P-CCPRE-Generators-References-CopilotReferenceGeneratorBase-DirectorySeparatorChar'></a>
### DirectorySeparatorChar `property`

##### Summary

Gets the platform-specific directory separator character.

<a name='P-CCPRE-Generators-References-CopilotReferenceGeneratorBase-GeneratorType'></a>
### GeneratorType `property`

##### Summary

Gets a
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
value that indicates which type of generator this instance represents.

<a name='M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-#cctor'></a>
### #cctor() `method`

##### Summary

Initializes `static` data or performs actions that
need to be performed once only for the
[CopilotReferenceGeneratorBase](#T-CCPRE-Generators-References-CopilotReferenceGeneratorBase 'CCPRE.Generators.References.CopilotReferenceGeneratorBase')
class.

##### Parameters

This method has no parameters.

##### Remarks

This constructor is called automatically prior to the first instance
being created or before any `static` members are referenced.



We've decorated this constructor with the `[Log(AttributeExclude = true)]`
attribute in order to simplify the logging output.

<a name='M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-AppendDirectorySeparator-System-String-'></a>
### AppendDirectorySeparator(path) `method`

##### Summary

Appends a directory separator to the specified path if one is not already
present.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the path with a trailing
directory separator, or [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if
`path` is blank or whitespace.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the path to a
folder, to which to append a directory separator. |

##### Remarks

A folder having the fully-qualified pathname specified by the value of the
`path` parameter must exist on the file system for this
method to work; otherwise, the method returns the
[Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') value.



If the specified folder `path` already has a trailing
directory separator, then this method is idempotent.



This method is used to ensure that path(s) are properly formatted for
[MakeRelativeUri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri.MakeRelativeUri 'System.Uri.MakeRelativeUri(System.Uri)') operation(s).



This method returns [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if
`path` is blank or whitespace.

<a name='M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-Generate-System-Object,System-String-'></a>
### Generate(selectedObject,solutionDirectory) `method`

##### Summary

Generates a GitHub Copilot reference string from the specified Visual
Studio object.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the formatted GitHub Copilot
reference, or [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if the reference
cannot be generated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| selectedObject | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | (Required.) Reference to an instance of [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object')
that represents the selected Visual Studio element (e.g.,
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem'), [Project](#T-EnvDTE-Project 'EnvDTE.Project'),
or CodeElement). |
| solutionDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the absolute
path to the solution directory. |

##### Remarks

This method validates all input(s) and returns
[Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if `selectedObject`
is `null`.



This method returns [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if
`solutionDirectory` is blank or whitespace.



This method delegates to the
[OnGenerate](#M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-OnGenerate-System-Object,System-String- 'CCPRE.Generators.References.CopilotReferenceGeneratorBase.OnGenerate(System.Object,System.String)')
template method after validation.

<a name='M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-OnGenerate-System-Object,System-String-'></a>
### OnGenerate(selectedObject,solutionDirectory) `method`

##### Summary

When overridden in a derived class, generates a GitHub Copilot reference
string from the specified Visual Studio object.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the formatted GitHub Copilot
reference, or [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if the reference
cannot be generated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| selectedObject | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | (Required.) Reference to an instance of [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object')
that represents the selected Visual Studio element. |
| solutionDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the absolute
path to the solution directory. |

##### Remarks

This method is called by the
[Generate](#M-CCPRE-Generators-References-CopilotReferenceGeneratorBase-Generate-System-Object,System-String- 'CCPRE.Generators.References.CopilotReferenceGeneratorBase.Generate(System.Object,System.String)')
method after input validation has been performed.



Implementer(s) can assume that `selectedObject` is
not `null` and that `solutionDirectory`
is not blank or whitespace.



Furthermore, implementers can also take it for granted that the folder whose
fully-qualified pathname is stored in the argument of the
`solutionDirectory` parameter refers to a folder that indeed
exists on the file system, and which contains the Visual Studio Solution (
`*.sln`) that is currently open in Visual Studio.



Implementer(s) should return [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if
the reference cannot be generated for any reason.

<a name='T-CCPRE-Generators-References-FileCopilotReferenceGenerator'></a>
## FileCopilotReferenceGenerator `type`

##### Namespace

CCPRE.Generators.References

##### Summary

Generates GitHub Copilot `#file:'path'` reference(s) for
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') object(s).

##### Remarks

This class implements the
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
interface for file-based reference(s).



This is a singleton class; call
[Instance](#P-CCPRE-Generators-References-FileCopilotReferenceGenerator-Instance 'CCPRE.Generators.References.FileCopilotReferenceGenerator.Instance')
to obtain a reference to the sole instance.

<a name='M-CCPRE-Generators-References-FileCopilotReferenceGenerator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Constructs a new instance of
[FileCopilotReferenceGenerator](#T-CCPRE-Generators-References-FileCopilotReferenceGenerator 'CCPRE.Generators.References.FileCopilotReferenceGenerator') and
returns a reference to it.

##### Parameters

This constructor has no parameters.

##### Remarks

This is an empty, `private` constructor to prohibit direct
allocation of this class, as it is a `Singleton` object accessible via the
[Instance](#P-CCPRE-Generators-References-FileCopilotReferenceGenerator-Instance 'CCPRE.Generators.References.FileCopilotReferenceGenerator.Instance')
property.

<a name='P-CCPRE-Generators-References-FileCopilotReferenceGenerator-GeneratorType'></a>
### GeneratorType `property`

##### Summary

Gets a
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
value that indicates which type of generator this instance represents.

<a name='P-CCPRE-Generators-References-FileCopilotReferenceGenerator-Instance'></a>
### Instance `property`

##### Summary

Gets a reference to the one and only instance of the object that implements the
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
interface for the
[File](#F-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType-File 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType.File')`Copilot Reference Generator` type.

<a name='M-CCPRE-Generators-References-FileCopilotReferenceGenerator-#cctor'></a>
### #cctor() `method`

##### Summary

Initializes `static` data or performs actions that
need to be performed once only for the
[FileCopilotReferenceGenerator](#T-CCPRE-Generators-References-FileCopilotReferenceGenerator 'CCPRE.Generators.References.FileCopilotReferenceGenerator')
class.

##### Parameters

This method has no parameters.

##### Remarks

This constructor is called automatically prior to the first instance
being created or before any `static` members are referenced.



We've decorated this constructor with the `[Log(AttributeExclude = true)]`
attribute in order to simplify the logging output.

<a name='M-CCPRE-Generators-References-FileCopilotReferenceGenerator-NormalizeSeparators-System-String-'></a>
### NormalizeSeparators(relativePath) `method`

##### Summary

Normalizes path separator(s) to forward slash(es) for GitHub Copilot
compatibility.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the normalized path with
forward slash(es), or [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if
`relativePath` is blank or whitespace.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| relativePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the relative
path to normalize. |

##### Remarks

This method returns [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if
`relativePath` is blank or whitespace.

<a name='M-CCPRE-Generators-References-FileCopilotReferenceGenerator-OnGenerate-System-Object,System-String-'></a>
### OnGenerate(selectedObject,solutionDirectory) `method`

##### Summary

When overridden in a derived class, generates a GitHub Copilot reference
string from the specified Visual Studio object.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the formatted GitHub Copilot
reference, or [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if the reference
cannot be generated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| selectedObject | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | (Required.) Reference to an instance of [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object')
that represents the selected Visual Studio element. |
| solutionDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the absolute
path to the solution directory. |

##### Remarks

This method expects `selectedObject` to be an instance
of [ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') representing a physical file.



This method returns [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if
`selectedObject` is not a
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem').



This method returns [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if the
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') does not represent a physical file.

<a name='M-CCPRE-Generators-References-FileCopilotReferenceGenerator-ToSolutionRelative-System-String,System-String-'></a>
### ToSolutionRelative(absolutePath,solutionDirectory) `method`

##### Summary

Converts an absolute file path to a solution-relative path.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the solution-relative path,
or [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if the conversion cannot be
performed.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| absolutePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the absolute
file path to convert. |
| solutionDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the absolute
path to the solution directory. |

##### Remarks

This method returns [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if
`absolutePath` is blank or whitespace.



This method returns [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') if
`solutionDirectory` is blank or whitespace.

<a name='T-CCPRE-Generators-References-Properties-Resources'></a>
## Resources `type`

##### Namespace

CCPRE.Generators.References.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-CCPRE-Generators-References-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-CCPRE-Generators-References-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.
