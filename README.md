# Space Marine UPC converter
Tool to decompress and compress UPC (\*.pc) files that contain locale texts. 
Resulting file (\*.locale) can be opened in text editor and edited. 
Be careful with spaces and tabs in the file, game accepts only original formatting.

## Usage
Tool requires .Net Framework 4.6.1 and DotNetZip. 

Available modes:
- -p: default, packs all \*.locale files in current directory and its subdirs back into UPC
- -pb: same as previous, but in big endian
- -p src dest: packs \*.locale file from src back into UPC
- -pb src dest: same as previous, but in big endian
- -u src dest: unpacks UPC file from src to dest
