const TrackingProvider = {
    init: function (selector) {
        const inputs = document.querySelectorAll(selector);
        console.log(`[TrackingProvider] Initialing for selector: ${selector}. Found ${inputs.length} elements.`);
        inputs.forEach(input => {
            input.addEventListener('input', (e) => this.applyFormat(e.target));
        });
    },
    
    applyFormat: function (input) {
        
        console.log("[TrackingProvider] Input event triggered. Current value:", input.value)
        
        let val = input.value.toUpperCase();
        
        if (val.length === 0) {
            console.log("[TrackingProvider] Value is empty< skipping.")
            return
        };
        
        const originalValue = input.value;
        
        let digitsOnly = originalValue.replace(/\D/g, '');
        
        if (digitsOnly.length === 0) {
            if (input.value !== "") input.value = "";
            return;
        }
        
        const formattedValue = "PL" + digitsOnly;
        
        if (originalValue !== formattedValue) {
            let cursorPosition = input.selectionStart;
            
            if (originalValue.length === 0 || (originalValue.length === 1 && /\d/.test(originalValue))) {
                cursorPosition += 2;
            }
            input.value = formattedValue;
            
            input.setSelectionRange(cursorPosition, cursorPosition);
            console.log("[TrackingProvider] DOM value updated to:", val);
        }
    }
};

document.addEventListener('DOMContentLoaded', () => {
    TrackingProvider.init('.tracking-input');
});