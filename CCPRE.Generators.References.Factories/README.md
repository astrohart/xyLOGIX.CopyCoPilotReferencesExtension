<a name='assembly'></a>
# CCPRE.Generators.References.Factories

## Contents

- [GetCopilotReferenceGenerator](#T-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator 'CCPRE.Generators.References.Factories.GetCopilotReferenceGenerator')
  - [CopilotReferenceGeneratorTypeValidator](#P-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator-CopilotReferenceGeneratorTypeValidator 'CCPRE.Generators.References.Factories.GetCopilotReferenceGenerator.CopilotReferenceGeneratorTypeValidator')
  - [#cctor()](#M-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator-#cctor 'CCPRE.Generators.References.Factories.GetCopilotReferenceGenerator.#cctor')
  - [OfType(type)](#M-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator-OfType-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType- 'CCPRE.Generators.References.Factories.GetCopilotReferenceGenerator.OfType(CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType)')
- [GetFileCopilotReferenceGenerator](#T-CCPRE-Generators-References-Factories-GetFileCopilotReferenceGenerator 'CCPRE.Generators.References.Factories.GetFileCopilotReferenceGenerator')
  - [#cctor()](#M-CCPRE-Generators-References-Factories-GetFileCopilotReferenceGenerator-#cctor 'CCPRE.Generators.References.Factories.GetFileCopilotReferenceGenerator.#cctor')
  - [SoleInstance()](#M-CCPRE-Generators-References-Factories-GetFileCopilotReferenceGenerator-SoleInstance 'CCPRE.Generators.References.Factories.GetFileCopilotReferenceGenerator.SoleInstance')
- [Resources](#T-CCPRE-Generators-References-Factories-Properties-Resources 'CCPRE.Generators.References.Factories.Properties.Resources')
  - [Culture](#P-CCPRE-Generators-References-Factories-Properties-Resources-Culture 'CCPRE.Generators.References.Factories.Properties.Resources.Culture')
  - [ResourceManager](#P-CCPRE-Generators-References-Factories-Properties-Resources-ResourceManager 'CCPRE.Generators.References.Factories.Properties.Resources.ResourceManager')

<a name='T-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator'></a>
## GetCopilotReferenceGenerator `type`

##### Namespace

CCPRE.Generators.References.Factories

##### Summary

Provides factory method(s) for obtaining
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
instance(s) based on the generator type.

##### Remarks

This `static` class implements the Strategy Factory
pattern for creating Copilot reference generator(s).

<a name='P-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator-CopilotReferenceGeneratorTypeValidator'></a>
### CopilotReferenceGeneratorTypeValidator `property`

##### Summary

Gets a reference to an instance of an object that implements the
[ICopilotReferenceGeneratorTypeValidator](#T-CCPRE-Generators-References-Validators-Interfaces-ICopilotReferenceGeneratorTypeValidator 'CCPRE.Generators.References.Validators.Interfaces.ICopilotReferenceGeneratorTypeValidator')
interface.

<a name='M-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator-#cctor'></a>
### #cctor() `method`

##### Summary

Initializes `static` data or performs actions that
need to be performed once only for the
[GetCopilotReferenceGenerator](#T-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator 'CCPRE.Generators.References.Factories.GetCopilotReferenceGenerator')
class.

##### Parameters

This method has no parameters.

##### Remarks

This constructor is called automatically prior to the first instance
being created or before any `static` members are referenced.



We've decorated this constructor with the `[Log(AttributeExclude = true)]`
attribute in order to simplify the logging output.

<a name='M-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator-OfType-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType-'></a>
### OfType(type) `method`

##### Summary

Obtains a reference to the sole instance of the
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
that corresponds to the specified `type`.

##### Returns

Reference to an instance of an object that implements the
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
interface, or `null` if no generator is available for
the specified `type`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType') | (Required.) A
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
value that specifies which generator to retrieve. |

##### Remarks

This method returns `null` if `type`
is
[Unknown](#F-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType-Unknown 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType.Unknown')
.



This method returns `null` if `type`
has an unrecognized value.

<a name='T-CCPRE-Generators-References-Factories-GetFileCopilotReferenceGenerator'></a>
## GetFileCopilotReferenceGenerator `type`

##### Namespace

CCPRE.Generators.References.Factories

##### Summary

Provides access to the one and only instance of the object that implements the
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
interface.

<a name='M-CCPRE-Generators-References-Factories-GetFileCopilotReferenceGenerator-#cctor'></a>
### #cctor() `method`

##### Summary

Initializes static data or performs actions that need to be performed once only
for the
[GetFileCopilotReferenceGenerator](#T-CCPRE-Generators-References-Factories-GetFileCopilotReferenceGenerator 'CCPRE.Generators.References.Factories.GetFileCopilotReferenceGenerator')
class.

##### Parameters

This method has no parameters.

##### Remarks

This constructor is called automatically prior to the first instance
being created or before any static members are referenced.

<a name='M-CCPRE-Generators-References-Factories-GetFileCopilotReferenceGenerator-SoleInstance'></a>
### SoleInstance() `method`

##### Summary

Obtains access to the sole instance of the object that implements the
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
interface, and returns a reference to it.

##### Returns

Reference to the one, and only, instance of the object that implements the
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
interface.

##### Parameters

This method has no parameters.

<a name='T-CCPRE-Generators-References-Factories-Properties-Resources'></a>
## Resources `type`

##### Namespace

CCPRE.Generators.References.Factories.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-CCPRE-Generators-References-Factories-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-CCPRE-Generators-References-Factories-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.
