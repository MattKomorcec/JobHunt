function toggleClass(element, classToToggle) {
    // If the element contains the class, remove it
    if (element.classList.contains(classToToggle)) {
        element.classList.remove(classToToggle)
    } else { // Otherwise add the class to the element
        element.classList.add(classToToggle)
    }
}

function main() {
    const darkSwitch = document.querySelector(".dark-switch")

    darkSwitch.addEventListener("click", function () {
        toggleClass(darkSwitch, "active")
        toggleClass(document.body, "dark")
    })
}

window.onload = main 
