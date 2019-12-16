function selectChanged(box) {
    var confirmation = true;
    if (box.selectedIndex == 3) {
        confirmation = confirm('Weet je zeker dat je het formulier wil verwijderen?');
    } else if (box.selectedIndex == 4) {
        confirmation = confirm('Weet je zeker dat je het formulier wil heropenen?');
    } 
    

    if (!confirmation)
        return;
       

    window.location.href = box.options[box.selectedIndex].value;
}

