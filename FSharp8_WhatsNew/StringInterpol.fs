module StringInterpol

let classAttr = "item-panel"

//CSS Copied from SO: 
// .item-panel:hover {background-color: #eee;}
let cssOld = 
    $""".{classAttr}:hover {{
    background-color: #eee;}}"""

(*
Mustache copied from SO:
    <div class="item-panel">
      <p>{{title}}</p> 
    </div>
*)
let templateOld = $"""
<div class="{classAttr}">
  <p>{{{{title}}}}</p>
</div>
"""

module WithFsharp8 = 

    let cssNew = 
        $$""".{{classAttr}}:hover {
            background-color: #eee;}"""

    let templateNew = $$$"""
    <div class="{{{classAttr}}}">
      <p>{{title}}</p> 
    </div>
    """