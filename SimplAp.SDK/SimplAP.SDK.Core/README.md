# Simple Api SDK
Adapter k zjednoduseniu integracie Simple Api.

## Pouzitie

### Vytvorenie instancie
```cs
var _service = new SimpleApiAIService(
    new SimpleApiAuthService(username, password, clientId, clientSecret, tenant)
);
```

### Pouzitie dostupnych metod

#### _ProcessImageFile_
Metoda k spracovaniu jednoducheho obrazoveho dokumentu.

```cs
MultiFileProcessingInput input = new MultiFileProcessingInput();
// ...Vypln vstupne hodnoty
AIModelType type = AIModelType.Vehicle; // alebo AIModelType.IdCard
MultiFileProcessingOutput output = await _service.ProcessImageFile(type, input);
```

##### Enumeracia (AIModelType)
##
| Hodnota | Opis |
| - | - |
| Vehicle | Rozpoznavanie dopravnych prostriedkov |
| IdCard | Rozpoznavanie dokladov |

##### [Vstup] (AIModelType, Trieda (MultiFileProcessingInput))
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| ImageData | byte[] | Datova reprezentacia obrazka, tzv. byte array |
| ImageType | ProcessedImageType | Typ obrazoveho dokumentu |
| ProcessesToRun | List<ImageAIProcessingType> | Zoznam typov akcii, ktore maju byt prevedene |

##### [Vystup] Trieda (MultiFileProcessingOutput)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| ProcessedFiles | List<ProcessedObjectFile> | Zoznam spracovanych obrazkov s navratovou hodnotou (Metadata extrahovane z obrazkov na zaklade zvolenych akcii)|

##### Enumeracia (ProcessedImageType)
##
| Hodnota | Opis |
| - | - |
| Image | Obrazovy dokument typu Obrazok |
| PDF | Obrazovy dokument typu PDF |

##### Enumeracia (ImageAIProcessingType)
##
| Hodnota | Opis |
| - | - |
| ObjectDetection | - |
| Scanner | - |
| FaceRecognition | Nie je podporovane |
| ObjectRotationAngle | - |
| ImageBlurDetection | Nie je podporovane |

##### Trieda (ProcessedObjectFile)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| PageNo | int? | Ak bol vstupnym obrazovym dokumentom viacstranovy dokument, tato hodnota oznacuje, na ktorej strane bol objekt zisteny |
| DetectedObjects | List<DetectedObjectExtended> | Zoznam rozpoznanych objektov |

##### Trieda (DetectedObjectExtended)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| BBox | BBox | Bounding Box zdetegovaneho objektu |
| Category | string | Kategoria zdetegovaneho objektu |
| Score | double | Miera istoty detekcie |
| PageNo | int? | Ak bol vstupnym obrazovym dokumentom viacstranovy dokument, tato hodnota oznacuje, na ktorej strane bol objekt zisteny |
| RollAngle | double? | Uhol, pod ktorym je zdetegovany objekt otoceny vzhladom na obrazok |
| DetectedFaces | IEnumerable<FaceAnnotationDto> | Zoznam zdetegovanych tvari |
| IdCardInfo | IdCardInfo | Zdetegovane data o doklade |
| IsImageBlurred | bool? | Urcuje kvalitu obrazu a skutocnost, ci sa ma obrazok nasnimat znova. |

##### Trieda (BBox)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| Xmax | double | - |
| Xmin | double | - |
| Ymax | double | - |
| Ymin | double | - |

##### Trieda (FaceAnnotationDto)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| BoundingPoly | BoundingPolyDto | - |
| FdBoundingPoly | BoundingPolyDto | - |
| Landmarks | IEnumerable<LandmarkDto> | - |
| RollAngle | float | - |
| PanAngle | float | - |
| TiltAngle | float | - |
| DetectionConfidence | float | - |
| LandmarkingConfidence | float | - |
| JoyLikelihood | FaceAnnotationLikelihood | - |
| SorrowLikelihood | FaceAnnotationLikelihood | - |
| AngerLikelihood | FaceAnnotationLikelihood | - |
| SurpriseLikelihood | FaceAnnotationLikelihood | - |
| UnderExposedLikelihood | FaceAnnotationLikelihood | - |
| BlurredLikelihood | FaceAnnotationLikelihood | - |
| HeadwearLikelihood | FaceAnnotationLikelihood | - |
| DetectedFaceImageBase64 | string | - |

##### Trieda (BoundingPolyDto)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| Vertices | IEnumerable<VertexDto> | - |
| NormalizedVertices | IEnumerable<NormalizedVertexDto> | - |

##### Trieda (VertexDto)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| X | int | - |
| Y | int | - |

##### Trieda (NormalizedVertexDto)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| X | float | - |
| Y | float | - |

##### Trieda (LandmarkDto)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| Type | LandmarkType | - |
| Position | PositionDto | - |

##### Enumeracia (LandmarkType)
##
| Hodnota | Opis |
| - | - |
| UnknownLandmark | - |
| LeftEye | - |
| RightEye | - |
| LeftOfLeftEyebrow | - |
| RightOfLeftEyebrow | - |
| LeftOfRightEyebrow | - |
| RightOfRightEyebrow | - |
| MidpointBetweenEyes | - |
| NoseTip | - |
| UpperLip | - |
| LowerLip | - |
| MouthLeft | - |
| MouthRight | - |
| MouthCenter | - |
| NoseBottomRight | - |
| NoseBottomLeft | - |
| NoseBottomCenter | - |
| LeftEyeTopBoundary | - |
| LeftEyeRightCorner | - |
| LeftEyeBottomBoundary | - |
| LeftEyeLeftCorner | - |
| RightEyeTopBoundary | - |
| RightEyeRightCorner | - |
| RightEyeBottomBoundary | - |
| RightEyeLeftCorner | - |
| LeftEyebrowUpperMidpoint | - |
| RightEyebrowUpperMidpoint | - |
| LeftEarTragion | - |
| RightEarTragion | - |
| LeftEyePupil | - |
| RightEyePupil | - |
| ForeheadGlabella | - |
| ChinGnathion | - |
| ChinLeftGonion | - |
| ChinRightGonion | - |
| LeftCheekCenter | - |
| RightCheekCenter | - |

##### Trieda (PositionDto)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| X | float | - |
| Y | float | - |
| Z | float | - |

##### Enumeracia (FaceAnnotationLikelihood)
##
| Hodnota | Opis |
| - | - |
| Unknown | - |
| VeryUnlikely | - |
| Unlikely | - |
| Possible | - |
| Likely | - |
| VeryLikely | - |

##### Trieda (IdCardInfo)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| NationalIdCardFrontInfo | NationalIdCardFrontInfo | - |
| NationalIdCardBackInfo | NationalIdCardBackInfo | - |
| DriversLicenseFrontInfo | DriversLicenseFrontInfo | - |
| DriversLicenseBackInfo | DriversLicenseBackInfo | - |
| SmallTechnicalLicenseBackInfo | SmallTechnicalLicenseBackInfo | - |
| SmallTechnicalLicenseFrontInfo | SmallTechnicalLicenseFrontInfo | - |
| PassportInfo | PassportInfo | - |
| CombinedExtractedInfo | Dictionary<string, List<object>> | - |
| WasCardLostOrStolen | bool? | - |

##### Trieda (NationalIdCardFrontInfo)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| FirstName | string | - |
| LastName | string | - |
| Gender | Gender | - |
| IdNumber | string | - |
| Nationality | string | - |
| BirthNumber | string | - |
| IssuedBy | string | - |
| IssuedDate | DateTime? | - |
| DateOfBirth | DateTime? | - |
| ExpiryDate | DateTime? | - |

##### Enumeracia (Gender)
##
| Hodnota | Opis |
| - | - |
| Male | - |
| Female | - |

##### Trieda (NationalIdCardBackInfo)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| Address | string | - |
| StreetName | string | - |
| City | string | - |
| StreetNumber | string | - |
| PostalCode | string | - |
| CountryCode | string | - |
| MaidenName | string | - |
| PlaceOfBirth | string | - |
| Title | string | - |
| BloodType | string | - |

##### Trieda (DriversLicenseFrontInfo)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| FirstName | string | - |
| LastName | string | - |
| DateOfBirth | DateTime? | - |
| PlaceOfBirth | string | - |
| IdNo | string | - |
| ValidFrom | DateTime? | - |
| ValidUntil | DateTime? | - |
| LicenseAllowedCategories | List<string> | - |

##### Trieda (DriversLicenseBackInfo)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| LicenseAllowedCategories | List<string> | - |

##### Trieda (SmallTechnicalLicenseFrontInfo)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| LicensePlate | string | - |
| Owner | string | - |
| Address | string | - |
| VIN | string | - |
| IdNo | string | - |
| IdNumber | string | - |
| StreetName | string | - |
| City | string | - |
| StreetNumber | string | - |
| PostalCode | string | - |
| FirstName | string | - |
| LastName | string | - |
| CompanyName | string | - |
| CountryCode | string | - |

##### Trieda (SmallTechnicalLicenseBackInfo)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| Manufacturer | string | - |
| Variant | string | - |
| Model | string | - |
| VIN | string | - |
| LargestWeight | string | - |
| OperationalWeight | string | - |
| ValidUntil | DateTime? | - |
| Category | string | - |
| TypeNumber | string | - |
| LargestTrailerTowingWeightO1Kg | int? | - |
| LargestTrailerTowingWeightO2Kg | int? | - |
| EngineVolume | string | - |
| EnginePerformance | string | - |
| FuelType | string | - |
| Paint | string | - |
| NumOfSeats | int? | - |
| MaxSpeed | int? | - |

##### Trieda (PassportInfo)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| IssuedBy | string | - |
| BirthNumber | string | - |
| IssuedDate | DateTime? | - |
| IdNumber | string | - |
| PlaceOfBirth | string | - |
| FirstName | string | - |
| LastName | string | - |
| ExpiryDate | DateTime? | - |
| DateOfBirth | DateTime? | - |
| CountryCode | string | - |
| Type | string | - |
| Gender | Gender? | - |
| Nationality | string | - |

#### _ProcessImageFileMultipart_
Metoda k spracovaniu obrazovych dokumentov s moznostou odosielania multipart udajov.

```cs
MultiFileProcessingMultipartInput input = new MultiFileProcessingMultipartInput();
// ...Vypln vstupne hodnoty
AIModelType type = AIModelType.Vehicle; // alebo AIModelType.IdCard
MultiFileProcessingOutput output = await _service.ProcessImageFileMultipart(type, input);
```

##### [Vstup] Trieda (AIModelType, (MultiFileProcessingMultipartInput))
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| FileStream | Stream | Datova reprezentacia obrazka, tzv. file stream |
| ImageType | ProcessedImageType | Typ obrazoveho dokumentu |
| ProcessesToRun | List<ImageAIProcessingType> | Zoznam typov akcii, ktore maju byt prevedene |

##### [Vystup] Trieda (MultiFileProcessingOutput)
##

#### _GetAvailableImageProcessing_
Metoda k zisteniu dostupnych typov akcii, ktore mozu byt pocas spracovania obrazoveho dokumentu pouzite.

```cs
List<AvailableProcessingDto> output = await _service.GetAvailableImageProcessing();
```

##### [Vystup] Zoznam tried (AvailableProcessingDto)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| Name | string | - |
| Description | string | - |
| ExampleUrl | string | - |
| ProductCode | string | Kod produktu |
| ProcessingTypeEnumValue | ImageAIProcessingType | Typ akcie, ktora ma byt prevedena |
| ProcessingTypeStringValue | string | Typ akcie, ktora ma byt prevedena (textova reprezentacia)|

#### _GetNumberOfCallsForPeriod_
Metoda k zisteniu poctu volani Simple Api za dane casove obdobie.

```cs
DateTimeOffset dateFrom = DateTimeOffset.Now.AddDays(-3);
DateTimeOffset dateTo = DateTimeOffset.Now;
GetNumberOfCallsForPeriodOutput output = await _service.GetNumberOfCallsForPeriod(dateFrom, dateTo);
```

##### [Vstup] (DateTimeOffset, DateTimeOffset)
##

##### [Vystup] Trieda (GetNumberOfCallsForPeriodOutput)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| TotalNumberOfCalls | int | - |
| CallsFrom | DateTimeOffset | - |
| CallsUntil | DateTimeOffset | - |
| CallsByProduct | List<NumberOfCallsPerProductDto> | - |

##### Trieda (NumberOfCallsPerProductDto)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| NumberOfCalls | int | - |
| ProductCode | string | Kod produktu |
| ProductName | string | Nazov produktu |

#### _GetNumberOfCallsPerDayForPeriod_
Metoda k zisteniu poctu volani Simple Api vzhladom na dni za dane obdobie.

```cs
DateTimeOffset dateFrom = DateTimeOffset.Now.AddDays(-3);
DateTimeOffset dateTo = DateTimeOffset.Now;
List<NumberOfCallsPerDayDto> output = await _service.GetNumberOfCallsPerDayForPeriod(dateFrom, dateTo);
```

##### [Vstup] (DateTimeOffset, DateTimeOffset)
##

##### [Vystup] Zoznam tried (NumberOfCallsPerDayDto)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| Day | DateTime | Datum a cas |
| NumberOfCallsPerDay | int | - |
