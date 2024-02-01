# Simple Api SDK
Adapter k zjednoduseniu integracie na sluzbu Simpl AP (https://api.simplap.com).

## Pouzitie

### Ziskanie pristupoveho tokenu (Access token)
```cs
var authService = new SimplAPAuthService(username, password, clientSecret, tenant);
var token = await authService.GetAccessTokenAsync();
```

Je dolezite si zapamatat ze token ma svoju expiraciu preto treba zapracovat logiku na obnovu tokenu po jeho vyprsani. Doba expiracie je dostupna v objekte tokenu. 

### Vytvorenie instancie sluzby
```cs
var _service = new SimplAPService();
```

### Pouzitie AI pre spracovanie obrazkov

#### _ProcessAI_
##### Spracovanie obrazka

```cs
// ...Typ AI modelu. Pre vyuzitie skennera na konkretne typy dokladov pouzite prosim AIModelType.IdCard
AIModelType modelType = AIModelType.Vehicle; // alebo AIModelType.IdCard
// ...Konfiguracia pozadovanej sluzby
var input = new ProcessingExtendedInput(
    AIModelType.IdCard,
    ProcessedImageType.Image,
    bytearray,
    ImageAIProcessingType.Scanner,
    ImageAIProcessingType.Detection,
    ImageAIProcessingType.ObjectRotationAngle, 
    ...);
// ...Zavolanie sluzby
ProcessingOutput output = await _service.ProcessAIAsync(input, token);
```

##### Spracovanie PDF suboru
```cs
// ...Typ AI modelu. Pre vyuzitie skennera na konkretne typy dokladov pouzite prosim AIModelType.IdCard
AIModelType modelType = AIModelType.Vehicle; // alebo AIModelType.IdCard
// ...Konfiguracia pozadovanej sluzby
var input = new ProcessingExtendedInput(
    AIModelType.IdCard,
    ProcessedImageType.Image,
    bytearray,
    ImageAIProcessingType.Scanner,
    ImageAIProcessingType.Detection,
    ImageAIProcessingType.ObjectRotationAngle, 
    ...);
// ...Zavolanie sluzby
ProcessingOutput output = await _service.ProcessAIAsync(input, token);
```

##### Enumeracia (AIModelType)
##
| Hodnota | Opis |
| - | - |
| Vehicle | Rozpoznavanie dopravnych prostriedkov |
| IdCard | Rozpoznavanie dokladov |

##### [Vstup] (AIModelType, Trieda (ProcessingExtendedInput))
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| ImageData | byte[] | Datova reprezentacia obrazka, tzv. byte array |
| ImageType | ProcessedImageType | Typ obrazoveho dokumentu |
| ProcessesToRun | IEnumerable&lt;ImageAIProcessingType&gt; | Zoznam typov akcii, ktore maju byt prevedene |
| DisableObjectSegmentation | bool | Nastavenie spracovania v segmentovanom rezime. Predvolena hodnota je false tj. spracovanie prebieha v segmentovanom rezime. Pri segmentovanom rezime spracovania sa jednotlive procesy (Skenovanie, Rozpoznanie Tvare, ...) vykonavaju na rozpoznane objekty cez cez spracovanie Detection. Segmentovane spracovanie je nevyhnutne pre proces skenovania konkretnych typov dokladov. |
| GenericScannerFieldsToUse | IEnumerable&lt;string&gt; | Zoznam typov poli ktore sa maju pouzit pre skenovanie. Tento parameter vyplnte iba pri nesegmentovanom spracovani, kedy mozete skenovat hocijaky typ dokumentu a hladat v nom konkretne typy rozpoznatelnych udajov. Zoznam dostupnych rozpoznatelnych udajov ziskate cez funkciu GetAvailableGenericScannerFields. |

##### Enumeracia (ProcessedImageType)
##
| Hodnota | Opis |
| - | - |
| Image | Obrazovy dokument typu Obrazok (predvolena hodnota) |
| PDF | Obrazovy dokument typu PDF |

##### Enumeracia (ImageAIProcessingType)
##
| Hodnota | Opis | Vyzaduje sluzbu |
| - | - | - |
| ObjectDetection | Rozpoznanie objektov | - |
| Scanner | Vytazovanie udajov (z dokladov) | ObjectDetection |
| ObjectRotationAngle | Rozpoznanie uhla otocena rozpoznaneho objektu | ObjectDetection |
| FaceRecognition | Rozpoznanie tvare | ObjectDetection |
| FaceExtraction | Extrakcia obrazka tvare | ObjectDetection, FaceRecognition, ObjectRotationAngle |
| ImageBlurDetection | Rozpoznanie rozmazanosti obrazka | - |
| CardLostOrStolenDetection | Zistenie ci bol doklad ukradnuty alebo strateny | ObjectDetection |
| BarcodeReader | Precitanie ciaroveho alebo QR kodu z obrazka | - |

##### [Vystup] Trieda (ProcessingOutput)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| Result | IEnumerable&lt;ProcessedEntities&gt; | Vysledok spracovania suboru. V pripade ze je subor PDF tak tento zoznam bude obsahovat viacero zaznamov na kazdu stranu suboru 1.  |

##### Trieda (ProcessedEntities) 
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| PageNo | int? | V pripade PDF dokumentu tu bude vyplnene cislo strany PDF dokumentu. Ak to bol obrazok tak hodnota bude null. |
| Entities | IEnumerable&lt;ProcessedEntity&gt; |  Vysledok spracovania. Moze byt viacero entit alebo ziadna. ( Napriklad viacero rozpoznanych objektov, rozpoznane ciarove kody, ... ) |

##### Trieda (ProcessedEntity) 
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| DetectedObject | PerformedProcessing&lt;DetectedObject&gt; | Rozpoznany objekt. V pripade ze rozpoznavanie prebehlo bude nastaveny priznak WasProcessingPerformed = true. Ak bolo rozpoznavanie uspesne, bude nastaveny priznak WasProcessingSuccessful = true. |
| IsImageBlurred | PerformedProcessing&lt;bool?&gt; | Rozpoznana rozmazanost obrazka. V pripade ze rozpoznavanie prebehlo bude nastaveny priznak WasProcessingPerformed = true. Ak bolo rozpoznavanie uspesne, bude nastaveny priznak WasProcessingSuccessful = true. |
| ScannedData | PerformedProcessing&lt;ScannerResult&gt; | Vytazene udaje z rozpoznaneho objektu. V pripade ze rozpoznavanie prebehlo bude nastaveny priznak WasProcessingPerformed = true. Ak bolo rozpoznavanie uspesne, bude nastaveny priznak WasProcessingSuccessful = true. |
| DetectedFaces | PerformedProcessing&lt;IEnumerable&lt;FaceAnnotationDto&gt;, FaceAnnotationDto&gt; | Rozpoznane tvare. V pripade ze je zapnute aj vytazovanie tvare tak aj base64 enkodovany obrazok tvare. V pripade ze rozpoznavanie prebehlo bude nastaveny priznak WasProcessingPerformed = true. Ak bolo rozpoznavanie uspesne, bude nastaveny priznak WasProcessingSuccessful = true. |
| RollAngle | PerformedProcessing&lt;double?&gt; | Rozpoznany uhol otocenia objektu. V pripade ze rozpoznavanie prebehlo bude nastaveny priznak WasProcessingPerformed = true. Ak bolo rozpoznavanie uspesne, bude nastaveny priznak WasProcessingSuccessful = true. |
| WasCardLostOrStolen | PerformedProcessing&lt;bool?&gt; | Rozpoznana vlastnost ci bol doklad strateny alebo ukradnuty. Aktualne vieme tuto vlastnost rozpoznat pre typ dokladu Pas a Obciansky preukaz. V pripade ze rozpoznavanie prebehlo bude nastaveny priznak WasProcessingPerformed = true. Ak bolo rozpoznavanie uspesne, bude nastaveny priznak WasProcessingSuccessful = true. |
| DetectedBarcode | PerformedProcessing&lt;BarcodeDto&gt; | Rozpoznany cirovy alebo QR kod. V pripade ze rozpoznavanie prebehlo bude nastaveny priznak WasProcessingPerformed = true. Ak bolo rozpoznavanie uspesne, bude nastaveny priznak WasProcessingSuccessful = true. |

##### Trieda (PerformedProcessing)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| WasProcessingPerformed | bool | Informacia ci bolo spracovanie vykonane |
| WasProcessingSuccessful | string | Ci bolo spracovanie uspesne |
| ImageAIProcessingType | ImageAIProcessingType | Typ spracovania na zaklade ktoreho prebehlo spracovanie |
| Result | TProcessedItem | Vysledok spracovania |

##### Trieda (DetectedObject)
##
| Atribut | Datovy typ | Opis |
| - | - | - |
| BBox | BBox | Bounding Box zdetegovaneho objektu |
| Category | string | Kategoria rozpoznaneho objektu |
| Score | double | Miera istoty detekcie |

##### Trieda (BBox)
##
| Atribut | Datovy typ |
| - | - |
| Xmax | double |
| Xmin | double |
| Ymax | double |
| Ymin | double |


##### Trieda (ScannerResult)
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

##### Enumeracia (Gender)
##
| Hodnota | Opis |
| - | - |
| Male | - |
| Female | - |


##### Trieda (FaceAnnotationDto)
##
| Atribut | Datovy typ |
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
| Atribut | Datovy typ |
| - | - | - |
| Vertices | IEnumerable&lt;VertexDto&gt; |
| NormalizedVertices | IEnumerable&lt;NormalizedVertexDto&gt; |

##### Trieda (VertexDto)
##
| Atribut | Datovy typ |
| - | - | 
| X | int |
| Y | int |

##### Trieda (NormalizedVertexDto)
##
| Atribut | Datovy typ |
| - | - |
| X | float |
| Y | float |

##### Trieda (LandmarkDto)
##
| Atribut | Datovy typ |
| - | - |
| Type | LandmarkType |
| Position | PositionDto |

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