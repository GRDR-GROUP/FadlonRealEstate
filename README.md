# Web Application Development - Final Project

# Real Estate Office WebSite
![Presentation Project](/UML/MainPage.png)

---
## Back-End
### Implements
C# | Javascript | Entity Framework | .NET | MVC

### Models & Controllers
Customers | Brokers | Deals | Properties | 

### SQL

GROUP BY

```scala
 public ActionResult Group()
        {
            var group = (from bo in db.Properties
                         group bo by bo.PropertyType into j
                         select new Group<string, Property> { Key = j.Key, Values = j });
            return View(group.ToList());
        }
```

JOIN

```scala
 public ActionResult Join()
        {
            string CustomerName = TempData["name"].ToString();
            var Delas = (from bo in db.Customers
                         join lo in db.Deals
                         on bo.CustomerID equals lo.CustomerID
                         where bo.CustomerFirstName.StartsWith(CustomerName)
                         select lo);
            return View();   
       }
```

---
## Front-End
### Implements
HTML\CSS | Bootsrap | jQuery | 

### HTML5
VIDEO
```scala
<div class="vid">
        <iframe frameborder="1" scrolling="yes" marginheight="0" marginwidth="0" width="1135" height="500" type="text/html" src="https://www.youtube.com/embed/rzSUKaz1lHY?autoplay=1&fs=1&iv_load_policy=3&showinfo=0&rel=0&cc_load_policy=0&start=0&end=0&vq=hd1080"></iframe>
    </div>
```
CANVAS
```scala
 <div class="canvas">
            <canvas id="myCanvas"></canvas>
            <img id="scream" width="0" height="0" src="~/Content/Resources/Imeges/service.jpg" alt="The Scream">
</div>
 <script>window.onload = function () {
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    var img = document.getElementById("scream");
    ctx.drawImage(img, 3, 3, 295, 150);}
</script>
```

### CSS3
Text-shadow | Transition | Multiple-columns | Font-face | Border-radius


### Accsess
Separation using TempData

```scala
 <ul class="nav navbar-nav navbar-right">
                    @if (TempData["Role"] != null)
                    {
                        <li>@Html.ActionLink("Log out", "Logout", "Home")</li>
                        if (TempData["Role"].ToString().Equals("Admin"))
                        {
                            <li style="margin-top:15px; margin-right:5px;"> @TempData["name"].ToString() </li><img src="~/Content/Resources/Team/Gal.jpeg" style="margin-top:1rem; width:30px; height:30px;" class="profile-image img-circle"> 
                        }
                        else
                        {
                            <li style="margin-top:15px; margin-right:5px;"> @TempData["name"].ToString() </li>
                        }
                        TempData.Keep();
                    }
                    else
                    {
                        <li>@Html.ActionLink("Login", "Login", "Home")</li>
                    }
                </ul>
```



### APIs

Google Maps

![Presentation Project](/UML/GoogleMaps.png)

```scala
  <div class="mapouter">
        <div class="gmap_canvas"><iframe width="1140" height="500" id="gmap_canvas" src="https://maps.google.com/maps?q=%D7%A8%D7%95%D7%98%D7%A9%D7%99%D7%9C%D7%93%2032%20%D7%91%D7%AA%20%D7%99%D7%9D&t=k&z=15&ie=UTF8&iwloc=&output=embed" frameborder="1" scrolling="yes" marginheight="10" marginwidth="0"></iframe></div>
    </div>
```

Facebook

![Presentation Project](/UML/Facebook.png)
```scala
  <div class="facebook">
        <h4>Like US! Recommend US! Share US!</h4>
        <div class="fb-like" data-href="https://www.facebook.com/Fadlon-Real-Estate-124652649071990/" data-width="" data-layout="button_count" data-action="like" data-size="large" data-share="false"></div> <div class="fb-like" data-href="https://www.facebook.com/Fadlon-Real-Estate-124652649071990/" data-width="200" data-layout="button_count" data-action="recommend" data-size="large" data-share="true"></div>
    </div>

    <div id="fb-root"></div>
    <script async defer crossorigin="anonymous" src="https://connect.facebook.net/en_US/sdk.js#xfbml=1&version=v7.0"></script>

```
### Web Service

Weather

![Presentation Project](/UML/Weather.png)
```scala
 <a class="weatherwidget-io" href="https://forecast7.com/en/32d0134d75/bat-yam/" data-label_1="BAT YAM" data-label_2="WEATHER" data-font="Times New Roman" data-icons="Climacons Animated" data-theme="weather_one">BAT YAM WEATHER</a>
    <script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = 'https://weatherwidget.io/js/widget.min.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'weatherwidget-io-js');</script>

```

### Statistics
```scala
 <Script>
 Highcharts.setOptions(Highcharts.theme);

            Highcharts.chart('container2', {
                data: {
                    table: 'datatable2'
                },
                colors: ['#2b908f', '#90ee7e', '#f45b5b', '#7798BF', '#aaeeee', '#ff0066',
                    '#eeaaee', '#55BF3B', '#DF5353', '#7798BF', '#aaeeee'],
                chart: {
                    type: 'pie'
                },

                title: {
                    text: 'Deals Statistics'
                },
                subtitle: {
                    text: 'By Property Name'
                },
                plotOptions: {
                    series: {
                        dataLabels: {
                            enabled: true,
                            format: '{point.name}: {point.percentage:.1f} %'
                        }
                    }

                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                }
            });
        </script>
```



---
## Build With
* [Microsoft Visual Studio ](https://visualstudio.microsoft.com/) 

## Authors
* **[Gal Jacobson](https://www.linkedin.com/in/jacobsongal/)** | **[Roey Miller](https://www.linkedin.com/in/roey-miller-046b68199/)** | **[ Dorel Fadlon ](https://www.linkedin.com/in/dorel-fadlon/)** | **[ Ron Fybish](https://www.linkedin.com/in/ron-fybish/)** 
