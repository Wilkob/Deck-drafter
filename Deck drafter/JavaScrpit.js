function encodJ(str)
{
    let output = ''
for (let i = 0; i < str.length; i += 8) {
    let segment = str.substring(i, i + 8)
    if (segment.length < 8) {
        segment = segment.padEnd(8, '0')
    }
    output += String.fromCharCode(parseInt(segment, 2))
}

return (btoa(output))
}



