# Simple Api SDK
Adapt�r k zjednodu�eniu integr�cie na slu�bu Simpl AP (https://api.simplap.com).

## Pou�itie

### Z�skanie pr�stupov�ho tokenu (Access token)
```cs
var authService = new SimplAPAuthService(username, password, clientSecret, tenant);
var token = await authService.GetAccessToken();
```

Je d�le�it� si zapam�ta� �e token m� svoju expir�ciu preto treba zapracova� logiku na obnovu tokenu po jeho vypr�an�. Doba expir�cie je dostupn� v objekte tokenu. 

### Vytvorenie in�tancie slu�by
```cs
var _service = new SimplAPService();
```

### Pou�itie AI pre spracovanie obr�zkov

#### _ProcessAI_
##### Spracovanie obr�zka

```cs
// ...Typ AI modelu. Pre vyu�itie skennera na konkr�tne typy dokladov pou�ite pros�m AIModelType.IdCard
AIModelType modelType = AIModelType.Vehicle; // alebo AIModelType.IdCard
// ...Konfigur�cia po�adovanej slu�by
var input = new ProcessingExtendedInput(
    AIModelType.IdCard,
    ProcessedImageType.Image,
    bytearray,
    ImageAIProcessingType.Scanner,
    ImageAIProcessingType.Detection,
    ImageAIProcessingType.ObjectRotationAngle, 
    ...);
// ...Zavolanie slu�by
ProcessingOutput output = await _service.ProcessAI(input, token);
```

##### Spracovanie PDF s�boru
```cs
// ...Typ AI modelu. Pre vyu�itie skennera na konkr�tne typy dokladov pou�ite pros�m AIModelType.IdCard
AIModelType modelType = AIModelType.Vehicle; // alebo AIModelType.IdCard
// ...Konfigur�cia po�adovanej slu�by
var input = new ProcessingExtendedInput(
    AIModelType.IdCard,
    ProcessedImageType.Image,
    bytearray,
    ImageAIProcessingType.Scanner,
    ImageAIProcessingType.Detection,
    ImageAIProcessingType.ObjectRotationAngle, 
    ...);
// ...Zavolanie slu�by
ProcessingOutput output = await _service.ProcessAI(input, token);
```

##### Enumer�cia (AIModelType)
##
| Hodnota | Opis |
| - | - |
| Vehicle | Rozpozn�vanie dopravn�ch prostriedkov |
| IdCard | Rozpozn�vanie dokladov |

##### [Vstup] (AIModelType, Trieda (ProcessingExtendedInput))
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| ImageData | byte[] | D�tov� reprezent�cia obr�zka, tzv. byte array |
| ImageType | ProcessedImageType | Typ obrazov�ho dokumentu |
| ProcessesToRun | IEnumerable&lt;ImageAIProcessingType&gt; | Zoznam typov akci�, ktor� maj� by� preveden� |
| DisableObjectSegmentation | bool | Nastavenie spracovania v segmentovanom re�ime. Predvolen� hodnota je false tj. spracovanie prebieha v segmentovanom re�ime. Pri segmentovanom re�ime spracovania sa jednotliv� procesy (Skenovanie, Rozpoznanie Tv�re, ...) vykon�vaj� na rozpoznan� objekty cez cez spracovanie Detection. Segmentovan� spracovanie je nevyhnutn� pre proces skenovania konkr�tnych typov dokladov. |
| GenericScannerFieldsToUse | IEnumerable&lt;string&gt; | Zoznam typov pol� ktor� sa maj� pou�i� pre skenovanie. Tento parameter vypl�te iba pri nesegmentovanom spracovan�, kedy m��ete skenova� hocijak� typ dokumentu a h�ada� v �om konkr�tne typy rozpoznate�n�ch �dajov. Zoznam dostupn�ch rozpoznate�n�ch �dajov z�skate cez funkciu GetAvailableGenericScannerFields. |

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
| FaceExtraction | Extrakcia obr�zka tv�re | ObjectDetection, FaceRecognition, ObjectRotationAngle |
| ImageBlurDetection | Rozpoznanie rozmazanosti obr�zka | - |
| CardLostOrStolenDetection | Zistenie �i bol doklad ukradnut� alebo straten� | ObjectDetection |
| BarcodeReader | Pre��tanie �iarov�ho alebo QR k�du z obr�zka | - |

##### [V�stup] Trieda (ProcessingOutput)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| Result | IEnumerable&lt;ProcessedEntities&gt; | V�sledok spracovania s�boru. V pr�pade �e je s�bor PDF tak tento zoznam bude obsahova� viacero z�znamov na ka�d� stranu s�boru 1.  |

##### Trieda (ProcessedEntities) 
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| PageNo | int? | V pr�pade PDF dokumentu tu bude vyplnen� ��slo strany PDF dokumentu. Ak to bol obr�zok tak hodnota bude null. |
| Entities | IEnumerable&lt;ProcessedEntity&gt; |  V�sledok spracovania. M��e by� viacero ent�t alebo �iadna. ( Napr�klad viacero rozpoznan�ch objektov, rozpoznan� �iarov� k�dy, ... ) |

##### Trieda (ProcessedEntity) 
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| DetectedObject | PerformedProcessing&lt;DetectedObject&gt; | Rozpoznan� objekt. V pr�pade �e rozpozn�vanie prebehlo bude nastaven� pr�znak WasProcessingPerformed = true. Ak bolo rozpozn�vanie �spe�n�, bude nastaven� pr�znak WasProcessingSuccessful = true. |
| IsImageBlurred | PerformedProcessing&lt;bool?&gt; | Rozpoznan� rozmazanos� obr�zka. V pr�pade �e rozpozn�vanie prebehlo bude nastaven� pr�znak WasProcessingPerformed = true. Ak bolo rozpozn�vanie �spe�n�, bude nastaven� pr�znak WasProcessingSuccessful = true. |
| ScannedData | PerformedProcessing&lt;ScannerResult&gt; | Vy�a�en� �daje z rozpoznan�ho objektu. V pr�pade �e rozpozn�vanie prebehlo bude nastaven� pr�znak WasProcessingPerformed = true. Ak bolo rozpozn�vanie �spe�n�, bude nastaven� pr�znak WasProcessingSuccessful = true. |
| DetectedFaces | PerformedProcessing&lt;IEnumerable&lt;FaceAnnotationDto&gt;, FaceAnnotationDto&gt; | Rozpoznan� tv�re. V pr�pade �e je zapnut� aj vy�a�ovanie tv�re tak aj base64 enk�dovan� obr�zok tv�re. V pr�pade �e rozpozn�vanie prebehlo bude nastaven� pr�znak WasProcessingPerformed = true. Ak bolo rozpozn�vanie �spe�n�, bude nastaven� pr�znak WasProcessingSuccessful = true. |
| RollAngle | PerformedProcessing&lt;double?&gt; | Rozpoznan� uhol oto�enia objektu. V pr�pade �e rozpozn�vanie prebehlo bude nastaven� pr�znak WasProcessingPerformed = true. Ak bolo rozpozn�vanie �spe�n�, bude nastaven� pr�znak WasProcessingSuccessful = true. |
| WasCardLostOrStolen | PerformedProcessing&lt;bool?&gt; | Rozpoznan� vlastnos� �i bol doklad straten� alebo ukradnut�. Aktu�lne vieme t�to vlastnos� rozpozna� pre typ dokladu Pas a Ob�iansky preukaz. V pr�pade �e rozpozn�vanie prebehlo bude nastaven� pr�znak WasProcessingPerformed = true. Ak bolo rozpozn�vanie �spe�n�, bude nastaven� pr�znak WasProcessingSuccessful = true. |
| DetectedBarcode | PerformedProcessing&lt;BarcodeDto&gt; | Rozpoznan� �irov� alebo QR k�d. V pr�pade �e rozpozn�vanie prebehlo bude nastaven� pr�znak WasProcessingPerformed = true. Ak bolo rozpozn�vanie �spe�n�, bude nastaven� pr�znak WasProcessingSuccessful = true. |

##### Trieda (PerformedProcessing)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| WasProcessingPerformed | bool | Inform�cia �i bolo spracovanie vykonan� |
| WasProcessingSuccessful | string | �i bolo spracovanie �spe�n� |
| ImageAIProcessingType | ImageAIProcessingType | Typ spracovania na z�klade ktor�ho prebehlo spracovanie |
| Result | TProcessedItem | V�sledok spracovania |

##### Trieda (DetectedObject)
##
| Atrib�t | D�tov� typ | Opis |
| - | - | - |
| BBox | BBox | Bounding Box zdetegovan�ho objektu |
| Category | string | Kateg�ria rozpoznan�ho objektu |
| Score | double | Miera istoty detekcie |

##### Trieda (BBox)
##
| Atrib�t | D�tov� typ |
| - | - |
| Xmax | double |
| Xmin | double |
| Ymax | double |
| Ymin | double |


##### Trieda (ScannerResult)
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

##### Enumer�cia (Gender)
##
| Hodnota | Opis |
| - | - |
| Male | - |
| Female | - |


##### Trieda (FaceAnnotationDto)
##
| Atrib�t | D�tov� typ |
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
| Atrib�t | D�tov� typ |
| - | - | - |
| Vertices | IEnumerable&lt;VertexDto&gt; |
| NormalizedVertices | IEnumerable&lt;NormalizedVertexDto&gt; |

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