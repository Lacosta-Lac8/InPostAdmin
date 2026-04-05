function handleInput(input) {
    let val = input.value;
    if (val.length === 0) return;
    if (/^\d/.test(val)) {
        input.value = "PL" + val; 
    }
    
    input.value = input.value.toUpperCase();
}