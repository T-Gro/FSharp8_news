module StringInterpol

let classAttr = "item-panel"

let cssOld = $""".{classAttr}:hover {{background-color: #eee;}}"""
let cssNew = $$""".{{classAttr}}:hover {background-color: #eee;}"""

let templateOld = $"""
<div class="{classAttr}">
  <p>{{{{title}}}}</p>
</div>
"""

let templateNew = $$$"""
<div class="{{{classAttr}}}">
  <p>{{title}}</p> 
</div>
"""