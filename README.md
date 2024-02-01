# Simple Api SDK
Adaptér k zjednodušeniu integrácie na službu Simpl AP (https://api.simplap.com).

## Použitie

### Získanie prístupového tokenu (Access token)
```cs
var authService = new SimplAPAuthService(username, password, clientSecret, tenant);
var token = await authService.GetAccessToken();
```

Je dôležité si zapamätať že token má svoju expiráciu preto treba zapracovať logiku na obnovu tokenu po jeho vypršaní. Doba expirácie je dostupná v objekte tokenu. 

### Vytvorenie inštancie služby
```cs
var _service = new SimplAPService();
```

### Použitie AI pre spracovanie obrázkov

#### _ProcessAI_
##### Spracovanie obrázka

```cs
// ...Typ AI modelu. Pre využitie skennera na konkrétne typy dokladov použite prosím AIModelType.IdCard
AIModelType modelType = AIModelType.Vehicle; // alebo AIModelType.IdCard
// ...Konfigurácia požadovanej služby
var input = new ProcessingExtendedInput(
    AIModelType.IdCard,
    ProcessedImageType.Image,
    bytearray,
    ImageAIProcessingType.Scanner,
    ImageAIProcessingType.Detection,
    ImageAIProcessingType.ObjectRotationAngle, 
    ...);
// ...Zavolanie služby
ProcessingOutput output = await _service.ProcessAI(input, token);
```

##### Spracovanie PDF súboru
```cs
// ...Typ AI modelu. Pre využitie skennera na konkrétne typy dokladov použite prosím AIModelType.IdCard
AIModelType modelType = AIModelType.Vehicle; // alebo AIModelType.IdCard
// ...Konfigurácia požadovanej služby
var input = new ProcessingExtendedInput(
    AIModelType.IdCard,
    ProcessedImageType.Image,
    bytearray,
    ImageAIProcessingType.Scanner,
    ImageAIProcessingType.Detection,
    ImageAIProcessingType.ObjectRotationAngle, 
    ...);
// ...Zavolanie služby
ProcessingOutput output = await _service.ProcessAI(input, token);
```

##### Enumerácia (AIModelType)
##
| Hodnota | Opis |
| - | - |
| Vehicle | Rozpoznávanie dopravných prostriedkov |
| IdCard | Rozpoznávanie dokladov |

##### [Vstup] (AIModelType, Trieda (ProcessingExtendedInput))
##
| Atribút | Dátový typ | Opis |
| - | - | - |
| ImageData | byte[] | Dátová reprezentácia obrázka, tzv. byte array |
| ImageType | ProcessedImageType | Typ obrazového dokumentu |
| ProcessesToRun | IEnumerable&lt;ImageAIProcessingType&gt; | Zoznam typov akcií, ktoré majú byť prevedené |
| DisableObjectSegmentation | bool | Nastavenie spracovania v segmentovanom režime. Predvolená hodnota je false tj. spracovanie prebieha v segmentovanom režime. Pri segmentovanom režime spracovania sa jednotlivé procesy (Skenovanie, Rozpoznanie Tváre, ...) vykonávajú na rozpoznané objekty cez cez spracovanie Detection. Segmentované spracovanie je nevyhnutné pre proces skenovania konkrétnych typov dokladov. |
| GenericScannerFieldsToUse | IEnumerable&lt;string&gt; | Zoznam typov polí ktoré sa majú použiť pre skenovanie. Tento parameter vyplňte iba pri nesegmentovanom spracovaní, kedy môžete skenovať hocijaký typ dokumentu a hľadať v ňom konkrétne typy rozpoznateľných údajov. Zoznam dostupných rozpoznateľných údajov získate cez funkciu GetAvailableGenericScannerFields. |

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
| FaceExtraction | Extrakcia obrázka tváre | ObjectDetection, FaceRecognition, ObjectRotationAngle |
| ImageBlurDetection | Rozpoznanie rozmazanosti obrázka | - |
| CardLostOrStolenDetection | Zistenie či bol doklad ukradnutý alebo stratený | ObjectDetection |
| BarcodeReader | Prečítanie čiarového alebo QR kódu z obrázka | - |

##### [Výstup] Trieda (ProcessingOutput)
##
| Atribút | Dátový typ | Opis |
| - | - | - |
| Result | IEnumerable&lt;ProcessedEntities&gt; | Výsledok spracovania súboru. V prípade že je súbor PDF tak tento zoznam bude obsahovať viacero záznamov na každú stranu súboru 1.  |

##### Trieda (ProcessedEntities) 
##
| Atribút | Dátový typ | Opis |
| - | - | - |
| PageNo | int? | V prípade PDF dokumentu tu bude vyplnené číslo strany PDF dokumentu. Ak to bol obrázok tak hodnota bude null. |
| Entities | IEnumerable&lt;ProcessedEntity&gt; |  Výsledok spracovania. Môže byť viacero entít alebo žiadna. ( Napríklad viacero rozpoznaných objektov, rozpoznané čiarové kódy, ... ) |

##### Trieda (ProcessedEntity) 
##
| Atribút | Dátový typ | Opis |
| - | - | - |
| DetectedObject | PerformedProcessing&lt;DetectedObject&gt; | Rozpoznaný objekt. V prípade že rozpoznávanie prebehlo bude nastavený príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavený príznak WasProcessingSuccessful = true. |
| IsImageBlurred | PerformedProcessing&lt;bool?&gt; | Rozpoznaná rozmazanosť obrázka. V prípade že rozpoznávanie prebehlo bude nastavený príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavený príznak WasProcessingSuccessful = true. |
| ScannedData | PerformedProcessing&lt;ScannerResult&gt; | Vyťažené údaje z rozpoznaného objektu. V prípade že rozpoznávanie prebehlo bude nastavený príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavený príznak WasProcessingSuccessful = true. |
| DetectedFaces | PerformedProcessing&lt;IEnumerable&lt;FaceAnnotationDto&gt;, FaceAnnotationDto&gt; | Rozpoznané tváre. V prípade že je zapnuté aj vyťažovanie tváre tak aj base64 enkódovaný obrázok tváre. V prípade že rozpoznávanie prebehlo bude nastavený príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavený príznak WasProcessingSuccessful = true. |
| RollAngle | PerformedProcessing&lt;double?&gt; | Rozpoznaný uhol otočenia objektu. V prípade že rozpoznávanie prebehlo bude nastavený príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavený príznak WasProcessingSuccessful = true. |
| WasCardLostOrStolen | PerformedProcessing&lt;bool?&gt; | Rozpoznaná vlastnosť či bol doklad stratený alebo ukradnutý. Aktuálne vieme túto vlastnosť rozpoznať pre typ dokladu Pas a Občiansky preukaz. V prípade že rozpoznávanie prebehlo bude nastavený príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavený príznak WasProcessingSuccessful = true. |
| DetectedBarcode | PerformedProcessing&lt;BarcodeDto&gt; | Rozpoznaný čirový alebo QR kód. V prípade že rozpoznávanie prebehlo bude nastavený príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavený príznak WasProcessingSuccessful = true. |

##### Trieda (PerformedProcessing)
##
| Atribút | Dátový typ | Opis |
| - | - | - |
| WasProcessingPerformed | bool | Informácia či bolo spracovanie vykonané |
| WasProcessingSuccessful | string | Či bolo spracovanie úspešné |
| ImageAIProcessingType | ImageAIProcessingType | Typ spracovania na základe ktorého prebehlo spracovanie |
| Result | TProcessedItem | Výsledok spracovania |

##### Trieda (DetectedObject)
##
| Atribút | Dátový typ | Opis |
| - | - | - |
| BBox | BBox | Bounding Box zdetegovaného objektu |
| Category | string | Kategória rozpoznaného objektu |
| Score | double | Miera istoty detekcie |

##### Trieda (BBox)
##
| Atribút | Dátový typ |
| - | - |
| Xmax | double |
| Xmin | double |
| Ymax | double |
| Ymin | double |


##### Trieda (ScannerResult)
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
| DateOfBirth | DateTime? | - |
| ValidFrom | DateTime? | - |
| ValidUntil | DateTime? | - |
| LicenseAllowedCategories | List&lt;string&gt; | - |
| LicensePlate | string | - |
| Owner | string | - |
| VIN | string | - |
| CompanyName | string | - |
| Manufacturer | string | - |
| Variant | string | - |
| Model | string | - |
| LargestWeight | string | - |
| OperationalWeight | string | - |
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
| Type | string | - |

##### Enumerácia (Gender)
##
| Hodnota | Opis |
| - | - |
| Male | - |
| Female | - |


##### Trieda (FaceAnnotationDto)
##
| Atribút | Dátový typ |
| - | - |
| BoundingPoly | BoundingPolyDto |
| FdBoundingPoly | BoundingPolyDto |
| Landmarks | IEnumerable&lt;LandmarkDto&gt; |
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
| Vertices | IEnumerable&lt;VertexDto&gt; |
| NormalizedVertices | IEnumerable&lt;NormalizedVertexDto&gt; |

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