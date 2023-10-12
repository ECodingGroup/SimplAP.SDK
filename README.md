# Simple Api SDK
Adapt�r k zjednodu�eniu integr�cie Simple Api.

## Pou�itie

### Vytvorenie in�tancie
```cs
var _service = new SimpleApiAIService(
    new SimpleApiAuthService(username, password, clientId, clientSecret, tenant)
);
```

### Pou�itie dostupn�ch met�d

#### _ProcessImageFile_
Met�da k spracovaniu jednoduch�ho obrazov�ho dokumentu.

```cs
MultiFileProcessingInput input = new MultiFileProcessingInput();
// ...Vypl� vstupn� hodnoty
AIModelType type = AIModelType.Vehicle; // alebo AIModelType.IdCard
MultiFileProcessingOutput output = await _service.ProcessImageFile(type, input);
```

##### Enumer�cia (AIModelType)
##
| Hodnota | Opis |
| - | - |
| Vehicle | Rozpozn�vanie dopravn�ch prostriedkov |
| IdCard | Rozpozn�vanie dokladov |

##### [Vstup] (AIModelType, Trieda (MultiFileProcessingInput))
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| ImageData | byte[] | D�tov� reprezent�cia obr�zka, tzv. byte array |
| ImageType | ProcessedImageType | Typ obrazov�ho dokumentu |
| ProcessesToRun | List<ImageAIProcessingType> | Zoznam typov akci�, ktor� maj� by� preveden� |

##### [V�stup] Trieda (MultiFileProcessingOutput)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| ProcessedFiles | List<ProcessedObjectFile> | Zoznam spracovan�ch obr�zkov s n�vratovou hodnotou (Metad�ta extrahovan� z obr�zkov na z�klade zvolen�ch akci�)|

##### Enumer�cia (ProcessedImageType)
##
| Hodnota | Opis |
| - | - |
| Image | Obrazov� dokument typu Obr�zok |
| PDF | Obrazov� dokument typu PDF |

##### Enumer�cia (ImageAIProcessingType)
##
| Hodnota | Opis |
| - | - |
| ObjectDetection | - |
| Scanner | - |
| FaceRecognition | Nie je podporovan� |
| ObjectRotationAngle | - |
| ImageBlurDetection | Nie je podporovan� |

##### Trieda (ProcessedObjectFile)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| PageNo | int? | Ak bol vstupn�m obrazov�m dokumentom viacstranov� dokument, t�to hodnota ozna�uje, na ktorej strane bol objekt zisten� |
| DetectedObjects | List<DetectedObjectExtended> | Zoznam rozpoznan�ch objektov |

##### Trieda (DetectedObjectExtended)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| BBox | BBox | Bounding Box zdetegovan�ho objektu |
| Category | string | Kateg�ria zdetegovan�ho objektu |
| Score | double | Miera istoty detekcie |
| PageNo | int? | Ak bol vstupn�m obrazov�m dokumentom viacstranov� dokument, t�to hodnota ozna�uje, na ktorej strane bol objekt zisten� |
| RollAngle | double? | Uhol, pod ktor�m je zdetegovan� objekt oto�en� vzh�adom na obr�zok |
| DetectedFaces | IEnumerable<FaceAnnotationDto> | Zoznam zdetegovan�ch tv�r� |
| IdCardInfo | IdCardInfo | Zdetegovan� d�ta o doklade |
| IsImageBlurred | bool? | Ur�uje kvalitu obrazu a skuto�nos�, �i sa m� obr�zok nasn�ma� znova. |

##### Trieda (BBox)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| Xmax | double | - |
| Xmin | double | - |
| Ymax | double | - |
| Ymin | double | - |

##### Trieda (FaceAnnotationDto)
##
| Atrib�t | D�tov� typ | Opis |
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
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| Vertices | IEnumerable<VertexDto> | - |
| NormalizedVertices | IEnumerable<NormalizedVertexDto> | - |

##### Trieda (VertexDto)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| X | int | - |
| Y | int | - |

##### Trieda (NormalizedVertexDto)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| X | float | - |
| Y | float | - |

##### Trieda (LandmarkDto)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| Type | LandmarkType | - |
| Position | PositionDto | - |

##### Enumer�cia (LandmarkType)
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
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| X | float | - |
| Y | float | - |
| Z | float | - |

##### Enumer�cia (FaceAnnotationLikelihood)
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
| Atrib�t | D�tov� typ | Opis |
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
| Atrib�t | D�tov� typ | Opis |
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

##### Enumer�cia (Gender)
##
| Hodnota | Opis |
| - | - |
| Male | - |
| Female | - |

##### Trieda (NationalIdCardBackInfo)
##
| Atrib�t | D�tov� typ | Opis |
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
| Atrib�t | D�tov� typ | Opis |
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
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| LicenseAllowedCategories | List<string> | - |

##### Trieda (SmallTechnicalLicenseFrontInfo)
##
| Atrib�t | D�tov� typ | Opis |
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
| Atrib�t | D�tov� typ | Opis |
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
| Atrib�t | D�tov� typ | Opis |
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
Met�da k spracovaniu obrazov�ch dokumentov s mo�nos�ou odosielania multipart �dajov.

```cs
MultiFileProcessingMultipartInput input = new MultiFileProcessingMultipartInput();
// ...Vypl� vstupn� hodnoty
AIModelType type = AIModelType.Vehicle; // alebo AIModelType.IdCard
MultiFileProcessingOutput output = await _service.ProcessImageFileMultipart(type, input);
```

##### [Vstup] Trieda (AIModelType, (MultiFileProcessingMultipartInput))
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| FileStream | Stream | D�tov� reprezent�cia obr�zka, tzv. file stream |
| ImageType | ProcessedImageType | Typ obrazov�ho dokumentu |
| ProcessesToRun | List<ImageAIProcessingType> | Zoznam typov akci�, ktor� maj� by� preveden� |

##### [V�stup] Trieda (MultiFileProcessingOutput)
##

#### _GetAvailableImageProcessing_
Met�da k zisteniu dostupn�ch typov akci�, ktor� m��u by� po�as spracovania obrazov�ho dokumentu pou�it�.

```cs
List<AvailableProcessingDto> output = await _service.GetAvailableImageProcessing();
```

##### [V�stup] Zoznam tried (AvailableProcessingDto)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| Name | string | - |
| Description | string | - |
| ExampleUrl | string | - |
| ProductCode | string | K�d produktu |
| ProcessingTypeEnumValue | ImageAIProcessingType | Typ akcie, ktor� m� by� preveden� |
| ProcessingTypeStringValue | string | Typ akcie, ktor� m� by� preveden� (textov� reprezent�cia)|

#### _GetNumberOfCallsForPeriod_
Met�da k zisteniu po�tu volan� Simple Api za dan� �asov� obdobie.

```cs
DateTimeOffset dateFrom = DateTimeOffset.Now.AddDays(-3);
DateTimeOffset dateTo = DateTimeOffset.Now;
GetNumberOfCallsForPeriodOutput output = await _service.GetNumberOfCallsForPeriod(dateFrom, dateTo);
```

##### [Vstup] (DateTimeOffset, DateTimeOffset)
##

##### [V�stup] Trieda (GetNumberOfCallsForPeriodOutput)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| TotalNumberOfCalls | int | - |
| CallsFrom | DateTimeOffset | - |
| CallsUntil | DateTimeOffset | - |
| CallsByProduct | List<NumberOfCallsPerProductDto> | - |

##### Trieda (NumberOfCallsPerProductDto)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| NumberOfCalls | int | - |
| ProductCode | string | K�d produktu |
| ProductName | string | N�zov produktu |

#### _GetNumberOfCallsPerDayForPeriod_
Met�da k zisteniu po�tu volan� Simple Api vzh�adom na dni za dan� obdobie.

```cs
DateTimeOffset dateFrom = DateTimeOffset.Now.AddDays(-3);
DateTimeOffset dateTo = DateTimeOffset.Now;
List<NumberOfCallsPerDayDto> output = await _service.GetNumberOfCallsPerDayForPeriod(dateFrom, dateTo);
```

##### [Vstup] (DateTimeOffset, DateTimeOffset)
##

##### [V�stup] Zoznam tried (NumberOfCallsPerDayDto)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| Day | DateTime | D�tum a �as |
| NumberOfCallsPerDay | int | - |
