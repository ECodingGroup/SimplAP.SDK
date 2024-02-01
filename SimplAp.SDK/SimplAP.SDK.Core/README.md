# Simple Api SDK
Adaptér k zjednodušeniu integrácie na slubu Simpl AP (https://api.simplap.com).

## Pouitie

### Získanie prístupového tokenu (Access token)
```cs
var authService = new SimplAPAuthService(username, password, clientSecret, tenant);
var token = await authService.GetAccessToken();
```

Je dôleité si zapamäta e token má svoju expiráciu preto treba zapracova logiku na obnovu tokenu po jeho vypršaní. Doba expirácie je dostupná v objekte tokenu. 

### Vytvorenie inštancie sluby
```cs
var _service = new SimplAPService();
```

### Pouitie AI pre spracovanie obrázkov

#### _ProcessAI_
##### Spracovanie obrázka

```cs
// ...Typ AI modelu. Pre vyuitie skennera na konkrétne typy dokladov pouite prosím AIModelType.IdCard
AIModelType modelType = AIModelType.Vehicle; // alebo AIModelType.IdCard
// ...Konfigurácia poadovanej sluby
var input = new ProcessingExtendedInput(
    AIModelType.IdCard,
    ProcessedImageType.Image,
    bytearray,
    ImageAIProcessingType.Scanner,
    ImageAIProcessingType.Detection,
    ImageAIProcessingType.ObjectRotationAngle, 
    ...);
// ...Zavolanie sluby
ProcessingOutput output = await _service.ProcessAI(input, token);
```

##### Spracovanie PDF súboru
```cs
// ...Typ AI modelu. Pre vyuitie skennera na konkrétne typy dokladov pouite prosím AIModelType.IdCard
AIModelType modelType = AIModelType.Vehicle; // alebo AIModelType.IdCard
// ...Konfigurácia poadovanej sluby
var input = new ProcessingExtendedInput(
    AIModelType.IdCard,
    ProcessedImageType.Image,
    bytearray,
    ImageAIProcessingType.Scanner,
    ImageAIProcessingType.Detection,
    ImageAIProcessingType.ObjectRotationAngle, 
    ...);
// ...Zavolanie sluby
ProcessingOutput output = await _service.ProcessAI(input, token);
```

##### Enumerácia (AIModelType)
##
| Hodnota | Opis |
| - | - |
| Vehicle | Rozpoznávanie dopravnıch prostriedkov |
| IdCard | Rozpoznávanie dokladov |

##### [Vstup] (AIModelType, Trieda (ProcessingExtendedInput))
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| ImageData | byte[] | Dátová reprezentácia obrázka, tzv. byte array |
| ImageType | ProcessedImageType | Typ obrazového dokumentu |
| ProcessesToRun | IEnumerable&lt;ImageAIProcessingType&gt; | Zoznam typov akcií, ktoré majú by prevedené |
| DisableObjectSegmentation | bool | Nastavenie spracovania v segmentovanom reime. Predvolená hodnota je false tj. spracovanie prebieha v segmentovanom reime. Pri segmentovanom reime spracovania sa jednotlivé procesy (Skenovanie, Rozpoznanie Tváre, ...) vykonávajú na rozpoznané objekty cez cez spracovanie Detection. Segmentované spracovanie je nevyhnutné pre proces skenovania konkrétnych typov dokladov. |
| GenericScannerFieldsToUse | IEnumerable&lt;string&gt; | Zoznam typov polí ktoré sa majú poui pre skenovanie. Tento parameter vyplòte iba pri nesegmentovanom spracovaní, kedy môete skenova hocijakı typ dokumentu a h¾ada v òom konkrétne typy rozpoznate¾nıch údajov. Zoznam dostupnıch rozpoznate¾nıch údajov získate cez funkciu GetAvailableGenericScannerFields. |

##### Enumerácia (ProcessedImageType)
##
| Hodnota | Opis |
| - | - |
| Image | Obrazovı dokument typu Obrázok (predvolená hodnota) |
| PDF | Obrazovı dokument typu PDF |

##### Enumerácia (ImageAIProcessingType)
##
| Hodnota | Opis | Vyaduje slubu |
| - | - | - |
| ObjectDetection | Rozpoznanie objektov | - |
| Scanner | Vyaovanie údajov (z dokladov) | ObjectDetection |
| ObjectRotationAngle | Rozpoznanie uhla otoèena rozpoznaného objektu | ObjectDetection |
| FaceRecognition | Rozpoznanie tváre | ObjectDetection |
| FaceExtraction | Extrakcia obrázka tváre | ObjectDetection, FaceRecognition, ObjectRotationAngle |
| ImageBlurDetection | Rozpoznanie rozmazanosti obrázka | - |
| CardLostOrStolenDetection | Zistenie èi bol doklad ukradnutı alebo stratenı | ObjectDetection |
| BarcodeReader | Preèítanie èiarového alebo QR kódu z obrázka | - |

##### [Vıstup] Trieda (ProcessingOutput)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| Result | IEnumerable&lt;ProcessedEntities&gt; | Vısledok spracovania súboru. V prípade e je súbor PDF tak tento zoznam bude obsahova viacero záznamov na kadú stranu súboru 1.  |

##### Trieda (ProcessedEntities) 
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| PageNo | int? | V prípade PDF dokumentu tu bude vyplnené èíslo strany PDF dokumentu. Ak to bol obrázok tak hodnota bude null. |
| Entities | IEnumerable&lt;ProcessedEntity&gt; |  Vısledok spracovania. Môe by viacero entít alebo iadna. ( Napríklad viacero rozpoznanıch objektov, rozpoznané èiarové kódy, ... ) |

##### Trieda (ProcessedEntity) 
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| DetectedObject | PerformedProcessing&lt;DetectedObject&gt; | Rozpoznanı objekt. V prípade e rozpoznávanie prebehlo bude nastavenı príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavenı príznak WasProcessingSuccessful = true. |
| IsImageBlurred | PerformedProcessing&lt;bool?&gt; | Rozpoznaná rozmazanos obrázka. V prípade e rozpoznávanie prebehlo bude nastavenı príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavenı príznak WasProcessingSuccessful = true. |
| ScannedData | PerformedProcessing&lt;ScannerResult&gt; | Vyaené údaje z rozpoznaného objektu. V prípade e rozpoznávanie prebehlo bude nastavenı príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavenı príznak WasProcessingSuccessful = true. |
| DetectedFaces | PerformedProcessing&lt;IEnumerable&lt;FaceAnnotationDto&gt;, FaceAnnotationDto&gt; | Rozpoznané tváre. V prípade e je zapnuté aj vyaovanie tváre tak aj base64 enkódovanı obrázok tváre. V prípade e rozpoznávanie prebehlo bude nastavenı príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavenı príznak WasProcessingSuccessful = true. |
| RollAngle | PerformedProcessing&lt;double?&gt; | Rozpoznanı uhol otoèenia objektu. V prípade e rozpoznávanie prebehlo bude nastavenı príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavenı príznak WasProcessingSuccessful = true. |
| WasCardLostOrStolen | PerformedProcessing&lt;bool?&gt; | Rozpoznaná vlastnos èi bol doklad stratenı alebo ukradnutı. Aktuálne vieme túto vlastnos rozpozna pre typ dokladu Pas a Obèiansky preukaz. V prípade e rozpoznávanie prebehlo bude nastavenı príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavenı príznak WasProcessingSuccessful = true. |
| DetectedBarcode | PerformedProcessing&lt;BarcodeDto&gt; | Rozpoznanı èirovı alebo QR kód. V prípade e rozpoznávanie prebehlo bude nastavenı príznak WasProcessingPerformed = true. Ak bolo rozpoznávanie úspešné, bude nastavenı príznak WasProcessingSuccessful = true. |

##### Trieda (PerformedProcessing)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| WasProcessingPerformed | bool | Informácia èi bolo spracovanie vykonané |
| WasProcessingSuccessful | string | Èi bolo spracovanie úspešné |
| ImageAIProcessingType | ImageAIProcessingType | Typ spracovania na základe ktorého prebehlo spracovanie |
| Result | TProcessedItem | Vısledok spracovania |

##### Trieda (DetectedObject)
##
| Atribút | Dátovı typ | Opis |
| - | - | - |
| BBox | BBox | Bounding Box zdetegovaného objektu |
| Category | string | Kategória rozpoznaného objektu |
| Score | double | Miera istoty detekcie |

##### Trieda (BBox)
##
| Atribút | Dátovı typ |
| - | - |
| Xmax | double |
| Xmin | double |
| Ymax | double |
| Ymin | double |


##### Trieda (ScannerResult)
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
| Atribút | Dátovı typ |
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
| Atribút | Dátovı typ |
| - | - | - |
| Vertices | IEnumerable&lt;VertexDto&gt; |
| NormalizedVertices | IEnumerable&lt;NormalizedVertexDto&gt; |

##### Trieda (VertexDto)
##
| Atribút | Dátovı typ |
| - | - | 
| X | int |
| Y | int |

##### Trieda (NormalizedVertexDto)
##
| Atribút | Dátovı typ |
| - | - |
| X | float |
| Y | float |

##### Trieda (LandmarkDto)
##
| Atribút | Dátovı typ |
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