
function validateDurationInput() {
    let durationInput = document.getElementById("duration-input");
    let resultParagraph = document.getElementById("validation-result");

    let duration = durationInput.value;

    if (/^([0-5][0-9]):([0-5][0-9])$/.test(duration)) {
        let [minutes, seconds] = duration.split(":");
        minutes = parseInt(minutes, 10);
        seconds = parseInt(seconds, 10);

        if (minutes >= 0 && minutes <= 59 && seconds >= 0 && seconds <= 59) {
            durationInput.value = `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
            resultParagraph.textContent = "";
        } else {
            resultParagraph.textContent = "Invalid minutes or seconds.";
        }
    } else {
        resultParagraph.textContent = "Invalid format. Use mm:ss.";
    }
}