# Simple Api SDK
Adaptér k zjednodušeniu integrácie Simple Api.

## Pouitie

### Vytvorenie inštancie
```cs
var _service = new SimpleApiAIService(
    new SimpleApiAuthService(username, password, clientId, clientSecret, tenant)
);
```

### Pouitie dostupnıch metód

#### _ProcessImageFile_
Metóda k spracovaniu jednoduchého obrazového dokumentu.

```cs
MultiFileProcessingInput input = new MultiFileProcessingInput();
// ...Vyplò vstupné hodnoty
AIModelType type = AIModelType.Vehicle; // alebo AIModelType.IdCard
MultiFileProcessingOutput output = await _service.ProcessImageFile(type, input);
```

##### Enumerácia (AIModelType)
##
| Hodnota | Opis |
| - | - |
| Vehicle | Rozpoznávanie dopravnıch prostriedkov |
| IdCard | Rozpoznávanie dokladov |

##### [Vstup] (AIModelType, Trieda (MultiFileProcessingInput))
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| ImageData | byte[] | Dátová reprezentácia obrázka, tzv. byte array |
| ImageType | ProcessedImageType | Typ obrazového dokumentu |
| ProcessesToRun | List<ImageAIProcessingType> | Zoznam typov akcií, ktoré majú by prevedené |

##### [Vıstup] Trieda (MultiFileProcessingOutput)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| ProcessedFiles | List<ProcessedObjectFile> | Zoznam spracovanıch obrázkov s návratovou hodnotou (Metadáta extrahované z obrázkov na základe zvolenıch akcií)|

##### Enumerácia (ProcessedImageType)
##
| Hodnota | Opis |
| - | - |
| Image | Obrazovı dokument typu Obrázok |
| PDF | Obrazovı dokument typu PDF |

##### Enumerácia (ImageAIProcessingType)
##
| Hodnota | Opis |
| - | - |
| ObjectDetection | - |
| Scanner | - |
| FaceRecognition | Nie je podporované |
| ObjectRotationAngle | - |
| ImageBlurDetection | Nie je podporované |

##### Trieda (ProcessedObjectFile)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| PageNo | int? | Ak bol vstupnım obrazovım dokumentom viacstranovı dokument, táto hodnota oznaèuje, na ktorej strane bol objekt zistenı |
| DetectedObjects | List<DetectedObjectExtended> | Zoznam rozpoznanıch objektov |

##### Trieda (DetectedObjectExtended)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| BBox | BBox | Bounding Box zdetegovaného objektu |
| Category | string | Kategória zdetegovaného objektu |
| Score | double | Miera istoty detekcie |
| PageNo | int? | Ak bol vstupnım obrazovım dokumentom viacstranovı dokument, táto hodnota oznaèuje, na ktorej strane bol objekt zistenı |
| RollAngle | double? | Uhol, pod ktorım je zdetegovanı objekt otoèenı vzh¾adom na obrázok |
| DetectedFaces | IEnumerable<FaceAnnotationDto> | Zoznam zdetegovanıch tvárí |
| IdCardInfo | IdCardInfo | Zdetegované dáta o doklade |
| IsImageBlurred | bool? | Urèuje kvalitu obrazu a skutoènos, èi sa má obrázok nasníma znova. |

##### Trieda (BBox)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| Xmax | double | - |
| Xmin | double | - |
| Ymax | double | - |
| Ymin | double | - |

##### Trieda (FaceAnnotationDto)
##
| Atribút | Dátovı typ | Opis |
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
| Atribút | Dátovı typ | Opis |
| - | - | - |
| Vertices | IEnumerable<VertexDto> | - |
| NormalizedVertices | IEnumerable<NormalizedVertexDto> | - |

##### Trieda (VertexDto)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| X | int | - |
| Y | int | - |

##### Trieda (NormalizedVertexDto)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| X | float | - |
| Y | float | - |

##### Trieda (LandmarkDto)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| Type | LandmarkType | - |
| Position | PositionDto | - |

##### Enumerácia (LandmarkType)
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
| Atribút | Dátovı typ | Opis |
| - | - | - |
| X | float | - |
| Y | float | - |
| Z | float | - |

##### Enumerácia (FaceAnnotationLikelihood)
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
| Atribút | Dátovı typ | Opis |
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
| Atribút | Dátovı typ | Opis |
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

##### Enumerácia (Gender)
##
| Hodnota | Opis |
| - | - |
| Male | - |
| Female | - |

##### Trieda (NationalIdCardBackInfo)
##
| Atribút | Dátovı typ | Opis |
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
| Atribút | Dátovı typ | Opis |
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
| Atribút | Dátovı typ | Opis |
| - | - | - |
| LicenseAllowedCategories | List<string> | - |

##### Trieda (SmallTechnicalLicenseFrontInfo)
##
| Atribút | Dátovı typ | Opis |
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
| Atribút | Dátovı typ | Opis |
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
| Atribút | Dátovı typ | Opis |
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
Metóda k spracovaniu obrazovıch dokumentov s monosou odosielania multipart údajov.

```cs
MultiFileProcessingMultipartInput input = new MultiFileProcessingMultipartInput();
// ...Vyplò vstupné hodnoty
AIModelType type = AIModelType.Vehicle; // alebo AIModelType.IdCard
MultiFileProcessingOutput output = await _service.ProcessImageFileMultipart(type, input);
```

##### [Vstup] Trieda (AIModelType, (MultiFileProcessingMultipartInput))
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| FileStream | Stream | Dátová reprezentácia obrázka, tzv. file stream |
| ImageType | ProcessedImageType | Typ obrazového dokumentu |
| ProcessesToRun | List<ImageAIProcessingType> | Zoznam typov akcií, ktoré majú by prevedené |

##### [Vıstup] Trieda (MultiFileProcessingOutput)
##

#### _GetAvailableImageProcessing_
Metóda k zisteniu dostupnıch typov akcií, ktoré môu by poèas spracovania obrazového dokumentu pouité.

```cs
List<AvailableProcessingDto> output = await _service.GetAvailableImageProcessing();
```

##### [Vıstup] Zoznam tried (AvailableProcessingDto)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| Name | string | - |
| Description | string | - |
| ExampleUrl | string | - |
| ProductCode | string | Kód produktu |
| ProcessingTypeEnumValue | ImageAIProcessingType | Typ akcie, ktorá má by prevedená |
| ProcessingTypeStringValue | string | Typ akcie, ktorá má by prevedená (textová reprezentácia)|

#### _GetNumberOfCallsForPeriod_
Metóda k zisteniu poètu volaní Simple Api za dané èasové obdobie.

```cs
DateTimeOffset dateFrom = DateTimeOffset.Now.AddDays(-3);
DateTimeOffset dateTo = DateTimeOffset.Now;
GetNumberOfCallsForPeriodOutput output = await _service.GetNumberOfCallsForPeriod(dateFrom, dateTo);
```

##### [Vstup] (DateTimeOffset, DateTimeOffset)
##

##### [Vıstup] Trieda (GetNumberOfCallsForPeriodOutput)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| TotalNumberOfCalls | int | - |
| CallsFrom | DateTimeOffset | - |
| CallsUntil | DateTimeOffset | - |
| CallsByProduct | List<NumberOfCallsPerProductDto> | - |

##### Trieda (NumberOfCallsPerProductDto)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| NumberOfCalls | int | - |
| ProductCode | string | Kód produktu |
| ProductName | string | Názov produktu |

#### _GetNumberOfCallsPerDayForPeriod_
Metóda k zisteniu poètu volaní Simple Api vzh¾adom na dni za dané obdobie.

```cs
DateTimeOffset dateFrom = DateTimeOffset.Now.AddDays(-3);
DateTimeOffset dateTo = DateTimeOffset.Now;
List<NumberOfCallsPerDayDto> output = await _service.GetNumberOfCallsPerDayForPeriod(dateFrom, dateTo);
```

##### [Vstup] (DateTimeOffset, DateTimeOffset)
##

##### [Vıstup] Zoznam tried (NumberOfCallsPerDayDto)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| Day | DateTime | Dátum a èas |
| NumberOfCallsPerDay | int | - |
