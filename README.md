# REST API 미니 테스트

이 리포지토리는 다양한 프레임워크를 사용한 간단한 REST API 구현을 위한 미니 테스트 프로젝트입니다.

## 프레임워크

각 참여자는 다음 프레임워크를 사용합니다:

- 해린 - Python (Django)
- 정우 - Python (FastAPI)
- 상화 - Go (Chi)
- 승재 - Go (Gin)
- 한규 - .NET
- 재혁 - Go (Fiber)

## 규칙

### 사용 가능한 함수명
- `createName`
- `getName`

### 제약 사항
- DB 사용 금지
- 데이터는 빈 배열(`[]`)에 저장하여 관리
- Repository 구조를 사용하여 구현
- Domain 생성 금지

## 구현 목표

간단한 테스트 구조를 생성하는 것이 목표이며, 각 프레임워크에서 동일한 기능을 구현하는 방식을 비교합니다.

## 추가 구현 사항

- 시간이 여유롭다면 유효성 검사(Validation)와 같은 기능들을 추가 구현하셔도 좋습니다.
- 프론트엔드는 원하는 웹 애플리케이션을 사용하셔도 좋고, curl을 통한 API 테스트도 가능합니다.


# MEMO

``` zsh
$ dotnet new --list                                              [8:30:01]

.NET 9.0을(를) 시작합니다.
---------------------
SDK 버전: 9.0.202

원격 분석
---------
.NET 도구는 사용자 환경 개선을 위해 사용량 현황 데이터를 수집합니다. Microsoft에서 데이터를 수집하여 커뮤니티와 공유합니다. 원하는 셸을 사용하여 DOTNET_CLI_TELEMETRY_OPTOUT 환경 변수를 '1' 또는 'true'로 설정하여 원격 분석을 옵트아웃할 수 있습니다.

.NET CLI 도구 원격 분석에 대한 자세한 내용은 https://aka.ms/dotnet-cli-telemetry를 참조하세요.

----------------
ASP.NET Core HTTPS 개발 인증서를 설치했습니다.
인증서를 신뢰하려면 'dotnet dev-certs https --trust'를 실행하세요.
HTTPS에 관한 자세한 정보: https://aka.ms/dotnet-https

----------------
첫 번째 앱 작성: https://aka.ms/dotnet-hello-world
새로운 기능 확인: https://aka.ms/dotnet-whats-new
설명서 살펴보기: https://aka.ms/dotnet-docs
GitHub에서 문제 보고 및 소스 찾기: https://github.com/dotnet/core
사용 가능한 명령을 보려면 'dotnet --help'를 사용하거나 https://aka.ms/dotnet-cli를 방문하세요.
--------------------------------------------------------------------------------------
워크로드를 확인하는 동안 문제가 발생했습니다. 자세한 내용을 확인하려면 "dotnet workload update"를 실행하세요.
경고: 'dotnet new --list' 사용은 중단되었습니다. 대신 'dotnet new list'을(를) 사용하세요.
자세한 내용은 다음을 실행하세요. 
   dotnet new list -h

이 템플릿은 입력 내용과 일치합니다. 

템플릿 이름                               약식 이름                   언어        태그                              
----------------------------------------  --------------------------  ----------  ----------------------------------
API 컨트롤러                              apicontroller               [C#]        Web/ASP.NET                       
ASP.NET Core gRPC 서비스                  grpc                        [C#]        Web/gRPC/API/Service              
ASP.NET Core 비어 있음                    web                         [C#],F#     Web/Empty                         
ASP.NET Core 웹 API                       webapi                      [C#],F#     Web/Web API/API/Service           
ASP.NET Core 웹 API(native AOT)           webapiaot                   [C#]        Web/Web API/API/Service           
ASP.NET Core 웹앱                         webapp,razor                [C#]        Web/MVC/Razor Pages               
ASP.NET Core 웹앱(Model-View-Controller)  mvc                         [C#],F#     Web/MVC                           
Blazor WebAssembly 독립 실행형 앱         blazorwasm                  [C#]        Web/Blazor/WebAssembly/PWA        
Blazor 웹앱                               blazor                      [C#]        Web/Blazor/WebAssembly            
dotnet gitignore 파일                     gitignore,.gitignore                    Config                            
dotnet 로컬 도구 매니페스트 파일          tool-manifest                           Config                            
EditorConfig 파일                         editorconfig,.editorconfig              Config                            
global.json 파일                          globaljson,global.json                  Config                            
MSBuild Directory.Build.props 파일        buildprops                              MSBuild/props                     
MSBuild Directory.Build.targets 파일      buildtargets                            MSBuild/props                     
MSBuild Directory.Packages.props 파일     packagesprops                           MSBuild/packages/props/CPM        
MSTest Playwright 테스트 프로젝트         mstest-playwright           [C#]        Test/MSTest/Playwright/Desktop/Web
MSTest 테스트 클래스                      mstest-class                [C#],F#,VB  Test/MSTest                       
MSTest 테스트 프로젝트                    mstest                      [C#],F#,VB  Test/MSTest/Desktop/Web           
MVC ViewImports                           viewimports                 [C#]        Web/ASP.NET                       
MVC ViewStart                             viewstart                   [C#]        Web/ASP.NET                       
MVC 컨트롤러                              mvccontroller               [C#]        Web/ASP.NET                       
NuGet 구성                                nugetconfig,nuget.config                Config                            
NUnit 3 테스트 프로젝트                   nunit                       [C#],F#,VB  Test/NUnit/Desktop/Web            
NUnit 3 테스트 항목                       nunit-test                  [C#],F#,VB  Test/NUnit                        
NUnit Playwright 테스트 프로젝트          nunit-playwright            [C#]        Test/NUnit/Playwright/Desktop/Web 
Razor 구성 요소                           razorcomponent              [C#]        Web/ASP.NET                       
Razor 뷰                                  view                        [C#]        Web/ASP.NET                       
Razor 클래스 라이브러리                   razorclasslib               [C#]        Web/Razor/Library                 
Razor 페이지                              page                        [C#]        Web/ASP.NET                       
xUnit 테스트 프로젝트                     xunit                       [C#],F#,VB  Test/xUnit/Desktop/Web            
솔루션 파일                               sln,solution                            Solution                          
웹 구성                                   webconfig                               Config                            
작업자 서비스                             worker                      [C#],F#     Common/Worker/Web                 
콘솔 앱                                   console                     [C#],F#,VB  Common/Console                    
클래스 라이브러리                         classlib                    [C#],F#,VB  Common/Library                    
프로토콜 버퍼 파일                        proto                                   Web/gRPC
```


``` zsh
dotnet new <템플릿> -n <프로젝트이름>
```

### 이런 방법도 존재한다
``` zsh
dotnet new install Clean.Architecture.Solution.Template
dotnet new ca-sln -n MyApp
```


## 실제 프로젝트 생성

### 1. API 프로젝트 생성
``` zsh
dotnet new webapi -n BlueberryHomework.Api
```

### 2. CQRS 관련 프로젝트들 추가
``` zsh
dotnet new classlib -n BlueberryHomework.Application
dotnet new classlib -n BlueberryHomework.Domain
dotnet new classlib -n BlueberryHomework.Infrastructure
```

### 3. 프로젝트 참조 연결
``` zsh
dotnet add BlueberryHomework.Api reference BlueberryHomework.Application BlueberryHomework.Domain
dotnet add BlueberryHomework.Application reference BlueberryHomework.Domain
dotnet add BlueberryHomework.Application reference BlueberryHomework.Infrastructure
```

``` zsh

```

``` zsh

```

``` zsh

```

``` zsh

```

``` zsh

```