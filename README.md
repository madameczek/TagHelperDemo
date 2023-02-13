# TagHelper demo application

This app illustrates use of custom pagination tag helper.  
Source code of tag helper is located in separate project. This helps to locate its files and dependencies.

Text on First/Last buttons can be defined directly in html.  
```html
<paging pagination="@Model" first-page-text="Pierwsza" last-page-text="Ostatnia" controller="FamilyNames" action="Index"></paging>
```
![](./pagination.png)  

The helper works with `IQueryable<T>` and fetches only data needed for current page. 

More on creating custom tag helpers for use with Razor engine can be found on 
[my blog](https://blog.adameczek.pl/index.php/2023/01/21/stronicowanie-w-razor-pages/)  
Yes, it is in Polish ;)