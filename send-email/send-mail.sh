#!/bin/bash

read -r -d '' HTML <<EOF
<h1>Test Email</h1>

<p>This is a test email with some <span style="color: blue"><em>slight</em></span> formatting.</p>

<p>Regards,</p>
<p><em>The Administrator</em></p>
EOF

echo "$HTML" | dotnet run -- -F "$1" -T "$2" -S "$3" -h localhost -p 25
