# !/bin/bash

# run: ejectutar el proyecto
run() {
    dotnet watch run --project MoogleServer
}

# report: compilar y generar el pdf del informe
report(){
    pdflatex -output-directory=Informe Informe/Informe.tex 
    echo ""
    echo "Informe.pdf generado"
}

# slides: compilar y generar el pdf de la presentación
slides(){
    pdflatex -output-directory=Presentacion Presentacion/Presentacion.tex
    echo ""
    echo "Presentacion.pdf generado"
}

# show_report: visualizar el informe
show_report(){

    dir=Informe/Informe.pdf

    if test -f $dir; then
        open_pdf "$1" "$dir"
    else
        report
        open_pdf "$1" "$dir"
    fi
}

# show_slides: visualizar la presentación
show_slides(){

    dir=Presentacion/Presentacion.pdf
    
    if test -f $dir; then
        open_pdf "$1" "$dir"
    else
        slides
        open_pdf "$1" "$dir"
    fi
}

open_pdf(){
    if [ "$1" = $"" ]
    then
        if [[ "$OSTYPE" == "linux-gnu" ]]; then
            xdg-open $2
        elif [[ "$OSTYPE" == "darwin"* ]]; then
            open $2
        else
            start $2
        fi
    else
        $1 $2
    fi
}

# clean: eliminar ficheros auxiliares que se generan en la compilación o ejecución del proyecto, o en la generación de los pdfs del informe o la presentación
clean(){
    find . -type f -name '*.aux' -delete
    find . -type f -name '*.fdb_latexmk' -delete
    find . -type f -name '*.fls' -delete
    find . -type f -name '*.log' -delete
    find . -type f -name '*.out' -delete
    find . -type f -name '*.synctex.gz' -delete
    find . -type f -name '*.nav' -delete
    find . -type f -name '*.snm' -delete
    find . -type f -name '*.toc' -delete
    find . -type f -name '*.vrb' -delete
    find . -type f -name '*.pdf' -delete
    echo "Todos los archivos auxiliares han sido borrados"
}

help() {
    echo "Comandos:"
    echo ""
    echo "1 || Run:       ejecutar el proyecto"
    echo "2 || Report:    compilar y generar el PDF del informe"
    echo "3 || Slides:    compilar y generar el PDF de las diapositivas"
    echo "4 || Show_report: ver el informe. Se puede especificar el visualizador de PDF de línea de comandos como argumento."
    echo "5 || Show_slides: ver las diapositivas. Se puede especificar el visualizador de PDF de línea de comandos como argumento."
    echo "6 || Clean:     eliminar archivos auxiliares"
    echo "7 || help:      mostrar este texto"
    echo "8 || clear:     limpiar la consola"
    echo "9 || exit:      salir del script"
}

cd ..

help

while read input ; 
do 
    command=$(echo "$input" | cut -d' ' -f1)
    arg=$(echo "$input" | cut -d' ' -f2) 

    # echo $command
    # echo $arg

    case $command in 
        run|1)
            run
            ;;
        
        report|2)
            report
            ;;
        
        slides|3)
            slides
            ;;
        
        show_report|4)
            if [ "$command" == "$arg" ]; then
                show_report
            else
                show_report "$arg"
            fi
            ;;
        
        show_slides|5)
            if [ "$command" == "$arg" ]; then
                show_slides
            else
                show_slides "$arg"
            fi
            ;;
        
        clean|6)
            clean
            ;;
        
        help|7)
            help
            ;;

        clear|8)
            clear
            ;;

        exit|9)
            exit
            ;;

        *)
            echo "invalid command"
    esac

done