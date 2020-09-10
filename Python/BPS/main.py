import bps


def rw_test():
    bps_file = bps.read(path + rf)

    for s in bps_file.find_all():
        print(s.name)
        for d in s.find_all():
            print(d.key + ":" + d.value)
        print("\n")

    bps.write(bps_file, path + wf)


def remove_data_test():
    bps_file = bps.read(path + rf)

    print(bps_file.find('section').remove('key'))

    bps.write(bps_file, path + wf)


def remove_section_test():
    bps_file = bps.read(path + rf)

    print(bps_file.remove('section'))

    bps.write(bps_file, path + wf)


def data_exists_test():
    bps_file = bps.read(path + rf)

    print(bps_file.find('section').exists('key'))
    print(bps_file.find('section').exists('key2'))

    bps.write(bps_file, path + wf)


def section_exists_test():
    bps_file = bps.read(path + rf)

    print(bps_file.exists('section'))
    print(bps_file.exists('section2'))

    bps.write(bps_file, path + wf)


def find_data_test():
    bps_file = bps.read(path + rf)

    d = bps_file.find('section').find('key')
    print(d.key + ":" + d.value)

    bps.write(bps_file, path + wf)


def find_section_test():
    bps_file = bps.read(path + rf)

    s = bps_file.find('section')
    print(s.name)
    for d in s.find_all():
        print(d.key + ":" + d.value)
    print("\n")

    bps.write(bps_file, path + wf)


# path = 'D:/Documentos/OneDrive/DESKTOP/BPSLib/'
path = 'C:/Users/Panificadora Larissa/OneDrive/DESKTOP/BPSLib/'
wf = 'write_test'
rf = 'read_test.bps'

rw_test()
# remove_data_test()
# remove_section_test()
# data_exists_test()
# section_exists_test()
# find_section_test()
# find_data_test()
