for file in plog/*
do
  filename=$(basename -- "$file")
  ~/plog-converter/build/plog-converter -a "" -t sarif -o "/home/msedlyarskiy/benchmark/tools/reports/pvs/${filename%.*}.sarif" $file
done