<a name='assembly'></a>
# CCPRE.Generators.References.Interfaces

## Contents

- [ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
  - [GeneratorType](#P-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator-GeneratorType 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator.GeneratorType')
  - [Generate(selectedObject,solutionDirectory)](#M-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator-Generate-System-Object,System-String- 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator.Generate(System.Object,System.String)')
- [Resources](#T-CCPRE-Generators-References-Interfaces-Properties-Resources 'CCPRE.Generators.References.Interfaces.Properties.Resources')
  - [Culture](#P-CCPRE-Generators-References-Interfaces-Properties-Resources-Culture 'CCPRE.Generators.References.Interfaces.Properties.Resources.Culture')
  - [ResourceManager](#P-CCPRE-Generators-References-Interfaces-Properties-Resources-ResourceManager 'CCPRE.Generators.References.Interfaces.Properties.Resources.ResourceManager')

<a name='T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator'></a>
## ICopilotReferenceGenerator `type`

##### Namespace

CCPRE.Generators.References.Interfaces

##### Summary

Defines the publicly-exposed events, methods and properties of an object
that generates GitHub Copilot reference(s) from Visual Studio selection(s).

<a name='P-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator-GeneratorType'></a>
### GeneratorType `property`

##### Summary

Gets a
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
value that indicates which type of generator this instance represents.

<a name='M-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator-Generate-System-Object,System-String-'></a>
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



Implementations must handle Visual Studio COM exceptions gracefully
and return [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') on error(s).

<a name='T-CCPRE-Generators-References-Interfaces-Properties-Resources'></a>
## Resources `type`

##### Namespace

CCPRE.Generators.References.Interfaces.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-CCPRE-Generators-References-Interfaces-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-CCPRE-Generators-References-Interfaces-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.
