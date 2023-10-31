# Simple Api SDK
Adaptér k zjednodušeniu integrácie na službu Simpl AP (https://api.simplap.com).

## Použitie

### Získanie prístupového tokenu (Access token)
```cs
var authService = new SimplAPAuthService(username, password, clientSecret, tenant);
var token = await authService.GetAccessToken();
```

### Vytvorenie inštancie
```cs
var _service = new SimplAPService();
```

### Použitie dostupných metód

#### _ProcessImageFile_
Metóda k spracovaniu jednoduchého obrazového dokumentu.

```cs
// ...Typ AI modelu
AIModelType modelType = AIModelType.Vehicle; // alebo AIModelType.IdCard
// ...Konfigurácia požadovanej služby
MultiFileProcessingInput input = new MultiFileProcessingInput(modelType)
{
    ImageData = ...,
    ProcessesToRun = new ImageAIProcessingType[] { ImageAIProcessingType.ObjectDetection, ... }
};
// ...Zavolanie služby
MultiFileProcessingOutput output = await _service.ProcessImageFile(input, token);
```

##### Enumerácia (AIModelType)
##
| Hodnota | Opis |
| - | - |
| Vehicle | Rozpoznávanie dopravných prostriedkov |
| IdCard | Rozpoznávanie dokladov |

##### [Vstup] (AIModelType, Trieda (MultiFileProcessingInput))
##
| Atribút | Dátový typ | Opis |
| - | - | - |
| ImageData | byte[] | Dátová reprezentácia obrázka, tzv. byte array |
| ImageType | ProcessedImageType | Typ obrazového dokumentu |
| ProcessesToRun | List<ImageAIProcessingType> | Zoznam typov akcií, ktoré majú byť prevedené |

##### [Výstup] Trieda (MultiFileProcessingOutput)
##
| Atribút | Dátový typ | Opis |
| - | - | - |
| ProcessedFiles | List<ProcessedObjectFile> | Zoznam spracovaných obrázkov s návratovou hodnotou (Metadáta extrahované z obrázkov na základe zvolených akcií)|

##### Enumerácia (ProcessedImageType)
##
| Hodnota | Opis |
| - | - |
| Image | Obrazový dokument typu Obrázok (predvolená hodnota) |
| PDF | Obrazový dokument typu PDF |

##### Enumerácia (ImageAIProcessingType)
##
| Hodnota | Opis | Vyžaduje službu |
| - | - | - |
| ObjectDetection | Rozpoznanie objektov | - |
| Scanner | Vyťažovanie údajov (z dokladov) | ObjectDetection |
| ObjectRotationAngle | Rozpoznanie uhla otočena rozpoznaného objektu | ObjectDetection |
| FaceRecognition | Rozpoznanie tváre | ObjectDetection |
| FaceExtraction | Extrakcia obrázka tváre - Pripravujeme | ObjectDetection, FaceRecognition |
| ImageBlurDetection | Rozpoznanie rozmazanosti obrázka - Pripravujeme | - |

##### Trieda (ProcessedObjectFile)
##
| Atribút | Dátový typ | Opis |
| - | - | - |
| PageNo | int? | Ak bol vstupným obrazovým dokumentom viacstranový dokument, táto hodnota označuje, na ktorej strane bol objekt rozpoznaný |
| DetectedObjects | List<DetectedObjectExtended> | Zoznam rozpoznaných objektov |

##### Trieda (DetectedObjectExtended)
##
| Atribút | Dátový typ | Opis |
| - | - | - |
| BBox | BBox | Bounding Box zdetegovaného objektu |
| Category | string | Kategória rozpoznaného objektu |
| Score | double | Miera istoty detekcie |
| RollAngle | double? | Uhol, pod ktorým je rozpoznaný objekt otočený vzhľadom na obrázok |
| DetectedFaces | IEnumerable<FaceAnnotationDto> | Zoznam rozpoznaných tvárí |
| IdCardInfo | IdCardInfo | Rozpoznané dáta o dokladoch |
| IsImageBlurred | bool? | Určuje kvalitu obrázka. |

##### Trieda (BBox)
##
| Atribút | Dátový typ |
| - | - |
| Xmax | double |
| Xmin | double |
| Ymax | double |
| Ymin | double |

##### Trieda (FaceAnnotationDto)
##
| Atribút | Dátový typ |
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
| Atribút | Dátový typ |
| - | - | - |
| Vertices | IEnumerable<VertexDto> |
| NormalizedVertices | IEnumerable<NormalizedVertexDto> |

##### Trieda (VertexDto)
##
| Atribút | Dátový typ |
| - | - | 
| X | int |
| Y | int |

##### Trieda (NormalizedVertexDto)
##
| Atribút | Dátový typ |
| - | - |
| X | float |
| Y | float |

##### Trieda (LandmarkDto)
##
| Atribút | Dátový typ |
| - | - |
| Type | LandmarkType |
| Position | PositionDto |

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
| Atribút | Dátový typ | Opis |
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
| Atribút | Dátový typ | Opis |
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
| Atribút | Dátový typ | Opis |
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
| Atribút | Dátový typ | Opis |
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
| Atribút | Dátový typ | Opis |
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
| Atribút | Dátový typ | Opis |
| - | - | - |
| LicenseAllowedCategories | List<string> | - |

##### Trieda (SmallTechnicalLicenseFrontInfo)
##
| Atribút | Dátový typ | Opis |
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
| Atribút | Dátový typ | Opis |
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
| Atribút | Dátový typ | Opis |
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
