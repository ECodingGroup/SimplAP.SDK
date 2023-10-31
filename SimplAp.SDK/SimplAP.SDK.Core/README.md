# Simple Api SDK
Adapt�r k zjednodu�eniu integr�cie na slu�bu Simpl AP (https://api.simplap.com).

## Pou�itie

### Z�skanie pr�stupov�ho tokenu (Access token)
```cs
var authService = new SimplAPAuthService(username, password, clientSecret, tenant);
var token = await authService.GetAccessToken();
```

### Vytvorenie in�tancie
```cs
var _service = new SimplAPService();
```

### Pou�itie dostupn�ch met�d

#### _ProcessImageFile_
Met�da k spracovaniu jednoduch�ho obrazov�ho dokumentu.

```cs
// ...Typ AI modelu
AIModelType modelType = AIModelType.Vehicle; // alebo AIModelType.IdCard
// ...Konfigur�cia po�adovanej slu�by
MultiFileProcessingInput input = new MultiFileProcessingInput(modelType)
{
    ImageData = ...,
    ProcessesToRun = new ImageAIProcessingType[] { ImageAIProcessingType.ObjectDetection, ... }
};
// ...Zavolanie slu�by
MultiFileProcessingOutput output = await _service.ProcessImageFile(input, token);
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
| Image | Obrazov� dokument typu Obr�zok (predvolen� hodnota) |
| PDF | Obrazov� dokument typu PDF |

##### Enumer�cia (ImageAIProcessingType)
##
| Hodnota | Opis | Vy�aduje slu�bu |
| - | - | - |
| ObjectDetection | Rozpoznanie objektov | - |
| Scanner | Vy�a�ovanie �dajov (z dokladov) | ObjectDetection |
| ObjectRotationAngle | Rozpoznanie uhla oto�ena rozpoznan�ho objektu | ObjectDetection |
| FaceRecognition | Rozpoznanie tv�re | ObjectDetection |
| FaceExtraction | Extrakcia obr�zka tv�re - Pripravujeme | ObjectDetection, FaceRecognition |
| ImageBlurDetection | Rozpoznanie rozmazanosti obr�zka - Pripravujeme | - |

##### Trieda (ProcessedObjectFile)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| PageNo | int? | Ak bol vstupn�m obrazov�m dokumentom viacstranov� dokument, t�to hodnota ozna�uje, na ktorej strane bol objekt rozpoznan� |
| DetectedObjects | List<DetectedObjectExtended> | Zoznam rozpoznan�ch objektov |

##### Trieda (DetectedObjectExtended)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| BBox | BBox | Bounding Box zdetegovan�ho objektu |
| Category | string | Kateg�ria rozpoznan�ho objektu |
| Score | double | Miera istoty detekcie |
| RollAngle | double? | Uhol, pod ktor�m je rozpoznan� objekt oto�en� vzh�adom na obr�zok |
| DetectedFaces | IEnumerable<FaceAnnotationDto> | Zoznam rozpoznan�ch tv�r� |
| IdCardInfo | IdCardInfo | Rozpoznan� d�ta o dokladoch |
| IsImageBlurred | bool? | Ur�uje kvalitu obr�zka. |

##### Trieda (BBox)
##
| Atrib�t | D�tov� typ |
| - | - |
| Xmax | double |
| Xmin | double |
| Ymax | double |
| Ymin | double |

##### Trieda (FaceAnnotationDto)
##
| Atrib�t | D�tov� typ |
| - | - |
| BoundingPoly | BoundingPolyDto |
| FdBoundingPoly | BoundingPolyDto |
| Landmarks | IEnumerable<LandmarkDto> |
| RollAngle | float |
| PanAngle | float |
| TiltAngle | float |
| DetectionConfidence | float |
| LandmarkingConfidence | float |
| JoyLikelihood | FaceAnnotationLikelihood |
| SorrowLikelihood | FaceAnnotationLikelihood |
| AngerLikelihood | FaceAnnotationLikelihood |
| SurpriseLikelihood | FaceAnnotationLikelihood |
| UnderExposedLikelihood | FaceAnnotationLikelihood |
| BlurredLikelihood | FaceAnnotationLikelihood |
| HeadwearLikelihood | FaceAnnotationLikelihood |
| DetectedFaceImageBase64 | string |

##### Trieda (BoundingPolyDto)
##
| Atrib�t | D�tov� typ |
| - | - | - |
| Vertices | IEnumerable<VertexDto> |
| NormalizedVertices | IEnumerable<NormalizedVertexDto> |

##### Trieda (VertexDto)
##
| Atrib�t | D�tov� typ |
| - | - | 
| X | int |
| Y | int |

##### Trieda (NormalizedVertexDto)
##
| Atrib�t | D�tov� typ |
| - | - |
| X | float |
| Y | float |

##### Trieda (LandmarkDto)
##
| Atrib�t | D�tov� typ |
| - | - |
| Type | LandmarkType |
| Position | PositionDto |

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
